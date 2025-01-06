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


        [HttpGet]
        public IActionResult EditBankWithMultipleBranch(int id)
        {
            var bank = _context.Banks.FirstOrDefault(b => b.BankId == id);

            if (bank == null)
            {
                return NotFound();
            }

            var viewModel = new EditBankWithMultipleBranchVM
            {
                Bank = new Bank
                {
                    BankId = bank.BankId,
                    BankName = bank.BankName,
                    BankAddress = bank.BankAddress,
                },
                ExistingLogo = bank.Logo,
                Branches = bank.Branches.Select(branch => new EditBranchVM
                {
                    BranchId = branch.BranchId,
                    BranchName = branch.BranchName,
                    IsActive =   branch.IsActive,
                    ExistingBranchLogo = branch.BranchLogo
                }).ToList()
                
            };

            return View(viewModel);
        }


        //[HttpPost]
        //public async Task<IActionResult> EditBankWithMultipleBranch(int id, EditBankWithMultipleBranchVM viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var bank = _context.Banks.FirstOrDefault(b => b.BankId == id);

        //        if (bank == null)
        //        {
        //            return NotFound();
        //        }


        //        bank.BankName = viewModel.Bank.BankName;
        //        bank.BankAddress = viewModel.Bank.BankAddress;

        //        if (viewModel.BankLogo != null)
        //        {
        //            string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");

        //            if (Directory.Exists(folder) == false)
        //            {
        //                Directory.CreateDirectory(folder);
        //            }

        //            string fileName = Guid.NewGuid().ToString() + "_" + viewModel.BankLogo.FileName;
        //            string filePath = Path.Combine(folder, fileName);

        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await viewModel.BankLogo.CopyToAsync(stream);
        //            }

        //            if (string.IsNullOrEmpty(bank.Logo) == false)
        //            {
        //                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), bank.Logo.TrimStart('/'));
        //                if (System.IO.File.Exists(oldFilePath))
        //                {
        //                    System.IO.File.Delete(oldFilePath);
        //                }
        //            }
        //            bank.Logo = $"/MyFile/images/{fileName}";
        //        }

        //        // Branch section
        //        foreach (var branchVM in viewModel.Branches)
        //        {
        //            var existingBranch = bank.Branches.FirstOrDefault(b => b.BranchId == branchVM.BranchId);

        //            if (existingBranch != null)
        //            {

        //                existingBranch.BranchName = branchVM.BranchName;
        //                existingBranch.IsActive = branchVM.IsActive;

        //                if (branchVM.BranchLogo != null)
        //                {
        //                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");

        //                    if (Directory.Exists(folder) == false)
        //                    {
        //                        Directory.CreateDirectory(folder);
        //                    }

        //                    string fileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
        //                    string filePath = Path.Combine(folder, fileName);

        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await branchVM.BranchLogo.CopyToAsync(stream);
        //                    }

        //                    if (string.IsNullOrEmpty(existingBranch.BranchLogo) == false)
        //                    {
        //                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), existingBranch.BranchLogo.TrimStart('/'));
        //                        if (System.IO.File.Exists(oldFilePath))
        //                        {
        //                            System.IO.File.Delete(oldFilePath);
        //                        }
        //                    }

        //                    existingBranch.BranchLogo = $"/MyFile/images/{fileName}";
        //                }
        //            }

        //            else
        //            {

        //                string branchLogo = existingBranch.BranchLogo;
        //                if (branchVM.BranchLogo != null)
        //                {
        //                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");

        //                    if (Directory.Exists(folder) == false)
        //                    {
        //                        Directory.CreateDirectory(folder);
        //                    }

        //                    string fileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
        //                    string filePath = Path.Combine(folder, fileName);

        //                    using (var stream = new FileStream(filePath, FileMode.Create))
        //                    {
        //                        await branchVM.BranchLogo.CopyToAsync(stream);
        //                    }

        //                    if (string.IsNullOrEmpty(branchLogo) == false)
        //                    {
        //                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), branchLogo.TrimStart('/'));
        //                        if (System.IO.File.Exists(oldFilePath))
        //                        {
        //                            System.IO.File.Delete(oldFilePath);
        //                        }
        //                    }

        //                    branchLogo = $"/MyFile/images/{fileName}";
        //                }

        //                var newBranch = new Branch
        //                {
        //                    BranchName = branchVM.BranchName,
        //                    IsActive = branchVM.IsActive,
        //                    BranchLogo = branchLogo
        //                };

        //                bank.Branches.Add(newBranch);
        //            }
        //        }

        //        _context.Banks.Update(bank);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(viewModel);
        //}

        [HttpPost]
        public async Task<IActionResult> EditBankWithMultipleBranchAjax(int id, EditBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == true)
            {
                var bank = _context.Banks.FirstOrDefault(b => b.BankId == id);

                if (bank == null)
                {
                    return Json(new { success = false, message = "Bank not found." });
                }

                bank.BankName = viewModel.Bank.BankName;
                bank.BankAddress = viewModel.Bank.BankAddress;

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

                    if (string.IsNullOrEmpty(bank.Logo) == false)
                    {
                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), bank.Logo.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    bank.Logo = $"/MyFile/images/{fileName}";
                }

                foreach (var branchVM in viewModel.Branches)
                {
                    var existingBranch = bank.Branches.FirstOrDefault(b => b.BranchId == branchVM.BranchId);

                    if (existingBranch != null)
                    {
                        existingBranch.BranchName = branchVM.BranchName;
                        existingBranch.IsActive = branchVM.IsActive;

                        if (branchVM.BranchLogo != null)
                        {
                            string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");

                            if (Directory.Exists(folder) == false)
                            {
                                Directory.CreateDirectory(folder);
                            }

                            string fileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
                            string filePath = Path.Combine(folder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await branchVM.BranchLogo.CopyToAsync(stream);
                            }

                            if (string.IsNullOrEmpty(existingBranch.BranchLogo) == false)
                            {
                                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), existingBranch.BranchLogo.TrimStart('/'));
                                if (System.IO.File.Exists(oldFilePath))
                                {
                                    System.IO.File.Delete(oldFilePath);
                                }
                            }

                            existingBranch.BranchLogo = $"/MyFile/images/{fileName}";
                        }
                    }
                    else
                    {
                        string branchLogo = null;

                        if (branchVM.BranchLogo != null)
                        {
                            string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");

                            if (Directory.Exists(folder) == false)
                            {
                                Directory.CreateDirectory(folder);
                            }

                            string fileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
                            string filePath = Path.Combine(folder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await branchVM.BranchLogo.CopyToAsync(stream);
                            }

                            branchLogo = $"/MyFile/images/{fileName}";
                        }

                        var newBranch = new Branch
                        {
                            BranchName = branchVM.BranchName,
                            IsActive = branchVM.IsActive,
                            BranchLogo = branchLogo
                        };

                        bank.Branches.Add(newBranch);
                    }
                }

                _context.Banks.Update(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank updated successfully!";

            }

            return Json(new { success =$"{isSuccess}", message =$"{message}" });
        }



        [HttpGet]
        public IActionResult CreateBankWithMultipleBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankWithMultipleBranch([FromForm] CreateBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                isSuccess = false;
                message = "Invalid data submitted!";
            }
            else
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

                foreach (var item in viewModel.Branches)
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
                            await item.BranchLogo.CopyToAsync(stream);
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
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank and branches created successfully!";

            }

            return Json(new { success =$"{isSuccess}", message =$"{message}" });
        }



        [HttpGet]
        public IActionResult CreateBankWithBranch()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateBankWithBranch([FromForm] CreateBankWithBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                isSuccess = false;
                message = "Invalid data submitted!";
            }

            else
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
                    if (Directory.Exists(folder) == false)
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
                isSuccess = true;
                message = "Bank and branch created successfully!";

            } 

            return Json(new { success =$"{isSuccess}", message =$"{message}" });
        }


        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Update(int? id)
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


        public IActionResult CreateMultiple()
        {
            return View(new List<CreateBankVM> { new CreateBankVM() }); 
        }

        

        [HttpPost]
        public async Task<IActionResult> CreateMultiple(List<CreateBankVM> banks)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted.!";

            if (ModelState.IsValid)
            {
                foreach (var viewModel in banks)
                {
                    string logoUrl = null;

                    if (viewModel.Logo != null)
                    {
                        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                        if (Directory.Exists(uploadsFolder) == false)
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Logo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName); 
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewModel.Logo.CopyToAsync(stream);
                        }
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
                isSuccess = true;
                message = "Data Submitted Successfully!";

            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }



        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBankVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
               isSuccess = false;
               message = "Invalid data submitted!";
            }

            else
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
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.Logo.CopyToAsync(stream);
                    }

                    logoUrl = $"/MyFile/images/{uniqueFileName}";
                }


                Bank bank = new Bank
                {
                    BankName = viewModel.BankName,
                    Logo = logoUrl,
                    BankAddress = viewModel.BankAddress
                };

                _context.Add(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank created successfully!";

            }
           
            return Json(new { success =$"{isSuccess}", message = $"{message}" });
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
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (id != viewModel.BankId)
            {
                isSuccess = false;
                message = "Bank not found!";

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
                    if (Directory.Exists(uploadsFolder) == false)
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.Logo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using ( var stream = new FileStream(filePath , FileMode.Create))
                    {
                        await viewModel.Logo.CopyToAsync(stream);
                    }

                   
                    if (string.IsNullOrEmpty(bank.Logo) == false)
                    {
                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), bank.Logo.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    photoUrl = $"/MyFile/images/{uniqueFileName}";
                }

                bank.BankName = viewModel.BankName;
                bank.BankAddress = viewModel.BankAddress;
                bank.Logo = photoUrl;

                _context.Update(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank details updated successfully!";
            }

            return Json(new { success =$"{isSuccess}", message = $"{message}" });
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.Include(b => b.Branches).FirstOrDefaultAsync(m => m.BankId == id);
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
                        if (Directory.Exists(uploadFolder) == false)
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        //string uniqueFileName = Guid.NewGuid().ToString() + "_" + branchVM.BranchLogo.FileName;
                        string uniqueFileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "_" + branchVM.BranchLogo.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await branchVM.BranchLogo.CopyToAsync(stream);
                        }

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
