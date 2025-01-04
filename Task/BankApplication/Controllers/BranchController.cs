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
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BankApplication.Controllers
{
    public class BranchController : Controller
    {
        private readonly BankDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BranchController(BankDbContext context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var branches = await _context.Branches.Include(b => b.Bank).ToListAsync();
            return View(branches);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.Include(b => b.Bank).FirstOrDefaultAsync(m => m.BranchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        
        public IActionResult Create()
        {
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CreateBranchVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (viewModel.BranchLogo != null)
                {
                    string uploadFolder = Path.Combine(_env.WebRootPath, "images");
                    if(Directory.Exists(uploadFolder) == false)
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.BranchLogo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    await viewModel.BranchLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));

                }
                 
                var branch = new Branch
                {
                     BranchName = viewModel.BranchName,
                     BranchLogo = uniqueFileName,
                     BankId = viewModel.BankId,
                     IsActive = viewModel.IsActive
                };
                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName");
            return View(viewModel);
        }


        public IActionResult CreateMultiple()
        {
            ViewData["Banks"] = new SelectList(_context.Banks, "BankId", "BankName");
            return View(new List<CreateBranchVM> { new CreateBranchVM() }); // Start with one branch by default
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMultiple(List<CreateBranchVM> branches)
        {
            if (ModelState.IsValid)
            {
                foreach (var viewModel in branches)
                {
                    string uniqueFileName = null;

                    if (viewModel.BranchLogo != null)
                    {
                        string uploadFolder = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.BranchLogo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);

                        await viewModel.BranchLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    }

                    var branch = new Branch
                    {
                        BranchName = viewModel.BranchName,
                        BranchLogo = uniqueFileName,
                        BankId = viewModel.BankId,
                        IsActive = viewModel.IsActive
                    };

                    _context.Add(branch);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Banks"] = new SelectList(_context.Banks, "BankId", "BankName");
            return View(branches);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new EditBranchVM
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                BankId = branch.BankId,
                IsActive = branch.IsActive,
                ExistingBranchLogo = branch.BranchLogo
            };

           
           
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", branch.BankId);
            return View(viewModel);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBranchVM viewModel)
        {
            if (id != viewModel.BranchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //check bank null
                var branch = await _context.Branches.FindAsync(id);

                if(branch == null)
                {
                    return NotFound();
                }

                branch.BranchName = viewModel.BranchName;
                branch.BankId = viewModel.BankId;
                branch.IsActive = viewModel.IsActive;

                if (viewModel.BranchLogo != null)
                {
                    //delete existing logo
                    if (string.IsNullOrEmpty(branch.BranchLogo) == false)
                    {

                        string exsitingFilePath = Path.Combine(_env.WebRootPath, "images", branch.BranchLogo);

                        if(System.IO.File.Exists(exsitingFilePath))
                        {
                            System.IO.File.Delete(exsitingFilePath);
                        }


                    }

                    //upload new logo
                    string uploadFolder = Path.Combine(_env.WebRootPath, "images");
                    string uniqueFilenName = Guid.NewGuid().ToString() + "_" + viewModel.BranchLogo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFilenName);
                    await viewModel.BranchLogo.CopyToAsync(new FileStream(filePath, FileMode.Create));

                    branch.BranchLogo = uniqueFilenName;

                }
                _context.Update(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            
            ViewData["BankId"] = new SelectList(_context.Banks, "BankId", "BankName", viewModel.BankId);
            return View(viewModel);
        }

   
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches.Include(b => b.Bank).FirstOrDefaultAsync(m => m.BranchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branches.FindAsync(id);
            
            if (branch != null)
            {
                _context.Branches.Remove(branch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.BranchId == id);
        }
    }
}
