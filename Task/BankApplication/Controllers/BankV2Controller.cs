using BankApplication.Data;
using BankApplication.Models.Entity;
using BankApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Controllers
{
    public class BankV2Controller : Controller
    {
        private readonly BankDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BankV2Controller(BankDbContext context, IWebHostEnvironment env)
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
        public IActionResult CreateBankWithMultipleBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankWithMultipleBranch( CreateBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
               
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }

            else
            {
                Bank bank = new Bank
                {
                    BankName = viewModel.Bank.BankName,
                    BankAddress = viewModel.Bank.BankAddress,
                    Branches = new List<Branch>()
                };

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

                    bank.Logo = $"/Myfile/images/{fileName}";
                }

                foreach (var item in viewModel.Branches)
                {
                    var branch = new Branch
                    {
                        BranchName = item.BranchName,
                        IsActive = item.IsActive,
                    };
                    
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

                        branch.BranchLogo = $"/Myfile/images/{fileName}";
                    }

                    bank.Branches.Add(branch);
                }

                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank and branches created successfully!";

            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
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
                    IsActive = branch.IsActive,
                    ExistingBranchLogo = branch.BranchLogo
                }).ToList()

            };

            return View(viewModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> EditBankWithMultipleBranch(int id, EditBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
            }

            else
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

                
                if (string.IsNullOrEmpty(viewModel.DeletedBranchIds) == false)
                {
                    var branchIdsToDelete = viewModel.DeletedBranchIds.Split(',').Select(int.Parse).ToList();
                    foreach (var branchId in branchIdsToDelete)
                    {
                        var branchToDelete = bank.Branches.FirstOrDefault(b => b.BranchId == branchId);
                        if (branchToDelete != null)
                        {
                            _context.Branches.Remove(branchToDelete);
                        }
                    }

                    viewModel.Branches = viewModel.Branches
                        .Where(b => b.BranchId == null || !viewModel.DeletedBranchIds.Split(',').Select(int.Parse).Contains(b.BranchId.Value))
                        .ToList();

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
                        var newBranch = new Branch
                        {
                            BranchName = branchVM.BranchName,
                            IsActive = branchVM.IsActive,
                        };
                        
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

                            newBranch.BranchLogo = $"/MyFile/images/{fileName}";
                        }

                        bank.Branches.Add(newBranch);
                    }
                }

                _context.Banks.Update(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank updated successfully!";

            }

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }
    }
}
