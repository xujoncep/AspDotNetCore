using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankApplication.Data;
using BankApplication.Models.Entity;
using BankApplication.Models.ViewModels;

namespace BankApplication.Controllers
{
    public class BankController : Controller
    {
        private readonly BankDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BankController(BankDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var banks = await _context.Banks.ToListAsync();
            return View(banks);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FirstOrDefaultAsync(m => m.BankId == id);
            var branches = await _context.Branches.Where(b => b.BankId == id).ToListAsync();
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }


        public IActionResult CreateMultiple()
        {
            return View(new List<CreateBankVM> { new CreateBankVM() }); // Start with one bank by default
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMultiple(List<CreateBankVM> banks)
        {
            if (ModelState.IsValid)
            {
                foreach (var viewModel in banks)
                {
                    string logoUrl = null;

                    if (viewModel.Logo != null)
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Logo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        await viewModel.Logo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        logoUrl = $"/MyFile/images/{uniqueFileName}";
                    }

                    Bank bank = new Bank
                    {
                        BankName = viewModel.BankName,
                        Logo = logoUrl,
                        BankAddress = viewModel.BankAddress
                    };

                    _context.Add(bank);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(banks);
        }

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateBankVM viewModel)
        {

            if (ModelState.IsValid)
            {
                string logoUrl = null;
                if (viewModel.Logo != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"MyFile","images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await viewModel.Logo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    logoUrl = ($"/Myfile/images/{uniqueFileName}");
                }

                Bank bank = new Bank
                {
                    BankName = viewModel.BankName,
                    Logo = logoUrl,
                    BankAddress = viewModel.BankAddress
                };

                _context.Add(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }


            var viewModel = new EditBankVM
            {
                BankId = bank.BankId,
                BankName = bank.BankName,
                ExistingLogo = bank.Logo,
                BankAddress = bank.BankAddress
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBankVM viewModel)
        {
            if (id !=viewModel.BankId)
            {
                return NotFound();
            }

            if ( ModelState.IsValid)
            {
                //null check
                var bank = await _context.Banks.FindAsync(id);
                if (bank == null)
                {
                    return NotFound();
                }

                string photoUrl = viewModel.ExistingLogo;

                if (viewModel.Logo != null)
                {
                    
                    //Delete existing logo
                    if (!string.IsNullOrEmpty(bank.Logo))
                    {
                        string ExistingLogoPath = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                        if(!Directory.Exists(ExistingLogoPath))
                        {
                            Directory.CreateDirectory(ExistingLogoPath);
                        }
                    

                   
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Logo.FileName;
                        string filePath = Path.Combine(ExistingLogoPath, uniqueFileName);
                        await viewModel.Logo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        photoUrl = ($"/Myfile/images/{uniqueFileName}");
                    }
                }
                
                bank.BankName = viewModel.BankName;
                bank.BankAddress = viewModel.BankAddress;
                bank.Logo = photoUrl;

                _context.Update(bank);
                 await _context.SaveChangesAsync();               
                 return RedirectToAction(nameof(Index));

            }


            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FirstOrDefaultAsync(m => m.BankId == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bank = await _context.Banks.FindAsync(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(int id)
        {
            return _context.Banks.Any(e => e.BankId == id);
        }


        [HttpGet]
        public IActionResult GetFile(string fileName)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images", fileName);

            if (System.IO.File.Exists(filePath) == false)
            {
                return NotFound();
            }

            var contentType = "image/jpeg";
            return PhysicalFile(filePath, contentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBranches(int BankId, List<CreateBranchVM> Branches)
        {
            if (Branches != null && Branches.Any())
            {
                foreach (var branchVM in Branches)
                {
                    string uniqueFileName = null;
                    if (branchVM.BranchLogo != null)
                    {
                        string uploadFolder = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        await branchVM.BranchLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }

                    var branch = new Branch
                    {
                        BranchName = branchVM.BranchName,
                        BranchLogo = uniqueFileName,
                        IsActive = branchVM.IsActive,
                        BankId = BankId
                    };

                    _context.Branches.Add(branch);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = BankId });
        }

    }
}
