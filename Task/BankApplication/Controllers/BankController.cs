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
            var banks = await _context.Banks.Include(b =>b.Branches).ToListAsync();
            return View(banks);
        }


        [HttpGet]
        public IActionResult CreateBankWithMultipleBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankWithMultipleBranch(CreateBankWithMultipleBranchVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string logo = null;

                if (viewModel.BankLogo != null)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                    if (Directory.Exists(folder) == false)
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Guid.NewGuid().ToString() + "_" + viewModel.BankLogo.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.BankLogo.CopyToAsync(stream);
                    }
                    logo = $"/Myfile/images/{fileName}";
                }

                Bank bank = new Bank
                {
                    BankName = viewModel.Bank.BankName,
                    BankAddress = viewModel.Bank.BankAddress,
                    Logo = logo,
                    Branches = new List<Branch>()
                };

                foreach ( var item in viewModel.Branches)
                {
                    string branchLogo = null;
                    if (item.BranchLogo != null)
                    {
                        string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                        if (Directory.Exists(folder) == false)
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string fileName = Guid.NewGuid().ToString() + "_" + item.BranchLogo.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            item.BranchLogo.CopyTo(stream);
                        }
                        branchLogo = $"/Myfile/images/{fileName}";
                    }

                    var branch = new Branch
                    {
                        BranchName = item.BranchName,
                        IsActive = item.IsActive,
                        BranchLogo = branchLogo,
                    };

                    bank.Branches.Add(branch);
                }

                _context.Banks.Add(bank);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpGet]
        public IActionResult CreateBankWithBranch()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBankWithBranch(CreateBankWithBranchVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

           
            string logo = null;

            if (viewModel.BankLogo != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                if (Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + viewModel.BankLogo.FileName;
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.BankLogo.CopyToAsync(stream);
                }
                logo = $"/Myfile/images/{fileName}";
            }

            var bank = new Bank
            {
                BankName = viewModel.BankName,
                BankAddress = viewModel.BankAddress,
                Logo = logo
            };

            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            
           
            string branchLogo = null;

            if (viewModel.BranchLogo != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                if (!Directory.Exists(folder) == false)
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + viewModel.BranchLogo.FileName;
                string filePath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.BranchLogo.CopyToAsync(stream);
                }
                branchLogo = $"/Myfile/images/{fileName}";
            }

            var branch = new Branch
            {
                BranchName = viewModel.BranchName,
                IsActive = viewModel.IsActive,
                BankId = bank.BankId,
                BranchLogo = branchLogo
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Update(int? id)
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
            return View(new List<CreateBankVM> { new CreateBankVM() }); 
        }

        

        [HttpPost]
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
                return Json(new { success = true, message = "Banks created successfully!" });
            }

            return Json(new { success = false, message = "Invalid data submitted." });
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBankVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Validation failed", errors = ModelState });
            }

            string logoUrl = null;

            // Handle logo upload
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

            // Create and save bank entity
            Bank bank = new Bank
            {
                BankName = viewModel.BankName,
                Logo = logoUrl,
                BankAddress = viewModel.BankAddress
            };

            _context.Add(bank);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Bank created successfully" });
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
        public async Task<IActionResult> Edit(int id, EditBankVM viewModel)
        {
            if (id != viewModel.BankId)
            {
                return Json(new { success = false, message = "Bank not found." });
            }

            if (ModelState.IsValid)
            {
                var bank = await _context.Banks.FindAsync(id);
                if (bank == null)
                {
                    return Json(new { success = false, message = "Bank not found." });
                }

                string photoUrl = bank.Logo;

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
                    photoUrl = $"/MyFile/images/{uniqueFileName}";
                }

                bank.BankName = viewModel.BankName;
                bank.BankAddress = viewModel.BankAddress;
                bank.Logo = photoUrl;

                _context.Update(bank);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Bank details updated successfully!" });
            }

            return Json(new { success = false, message = "Invalid data submitted." });
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
                    string photoUrl = null;
                    if (branchVM.BranchLogo != null)
                    {
                        string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(),"MyFile", "images");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        await branchVM.BranchLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                        photoUrl = $"/MyFile/images/{uniqueFileName}";
                    }

                    var branch = new Branch
                    {
                        BranchName = branchVM.BranchName,
                        BranchLogo = photoUrl,
                        IsActive = branchVM.IsActive,
                        BankId = BankId
                    };
                    _context.Branches.Add(branch);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Update", new { id = BankId });
        }

    }
}
