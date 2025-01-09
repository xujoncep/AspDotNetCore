using BankApplication.Data;
using BankApplication.Models.Entity;
using BankApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Controllers
{
    public class BankV3Controller : Controller
    {

        private readonly BankDbContext _context;
        public BankV3Controller(BankDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var banks = await _context.Banks.ToListAsync();

            return View(banks);
        }

        [HttpGet]
        public async Task<IActionResult> EditBankWithBranch(int id)
        {
            var bank = await _context.Banks.FindAsync(id);

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
                    BankAddress = bank.BankAddress
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
        public async Task<IActionResult> EditBankWithBranch(int id, EditBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Bank and Branches Invalid Info!";

            if(ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);

                foreach(var error in errors)
                {
                    Console.WriteLine(error);
                }
               
            }

            else
            {
                var bank = await _context.Banks.FindAsync(id);

                if(bank == null)
                {
                    return NotFound();
                }

                bank.BankName = viewModel.Bank.BankName;
                bank.BankAddress = viewModel.Bank.BankAddress;

                if (viewModel.BankLogo != null)
                {
                    string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile","images");
                    if(Directory.Exists(folder) == false)
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = DateTime.UtcNow.ToString("yyyyMMDDHHmmssfff") +"_" + viewModel.BankLogo.FileName;
                    string filePath = Path.Combine(folder,fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.BankLogo.CopyToAsync(stream);
                    }

                    if( string.IsNullOrEmpty(bank.Logo) == false)
                    {
                        string existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images", Path.GetFileName(bank.Logo));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                    }
                    bank.Logo = $"/MyFile/images/{fileName}";
                }

                if(string.IsNullOrEmpty(viewModel.DeletedBranchIds) == false)
                {
                    var deletedBranchIds = viewModel.DeletedBranchIds.Split(',').Select(int.Parse).ToList();

                    foreach (var branchId in deletedBranchIds)
                    {
                        var branch = await _context.Branches.FindAsync(branchId);
                        if (branch != null)
                        {
                            if (string.IsNullOrEmpty(branch.BranchLogo) == false)
                            {
                                string existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images", Path.GetFileName(branch.BranchLogo));
                                if (System.IO.File.Exists(existingFilePath))
                                {
                                    System.IO.File.Delete(existingFilePath);
                                }
                            }
                            _context.Branches.Remove(branch);
                        }
                    }

                    viewModel.Branches = viewModel.Branches
                        .Where(b => b.BranchId == null || !viewModel.DeletedBranchIds.Split(',').Select(int.Parse).Contains(b.BranchId.Value)).ToList();

                }

                foreach (var branch in viewModel.Branches)
                {
                    var existingBranch = bank.Branches.FirstOrDefault(b => b.BranchId == branch.BranchId);

                    if (existingBranch != null)
                    {
                       existingBranch.BranchName = branch.BranchName;
                       existingBranch.IsActive = branch.IsActive;

                        
                        if (branch.BranchLogo != null)
                        {
                            string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                            if (Directory.Exists(folder) == false)
                            {
                                Directory.CreateDirectory(folder);
                            }

                            string fileName = DateTime.UtcNow.ToString("yyyyMMDDHHmmssfff") + "_" + branch.BranchLogo.FileName;
                            string filePath = Path.Combine(folder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await branch.BranchLogo.CopyToAsync(stream);
                            }

                            if (string.IsNullOrEmpty(existingBranch.BranchLogo) == false)
                            {
                                string existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images", Path.GetFileName(existingBranch.BranchLogo));
                                if (System.IO.File.Exists(existingFilePath))
                                {
                                    System.IO.File.Delete(existingFilePath);
                                }
                            }
                            existingBranch.BranchLogo = $"/MyFile/images/{fileName}";
                        }

                    }

                    else
                    {
                        Branch newBranch = new Branch
                        {
                            BranchName = branch.BranchName,
                            IsActive = branch.IsActive,
                        };
                        
                        if (branch.BranchLogo != null)
                        {
                            string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                            if (Directory.Exists(folder) == false)
                            {
                                Directory.CreateDirectory(folder);
                            }

                            string fileName = DateTime.UtcNow.ToString("yyyyMMDDHHmmssfff") + "_" + branch.BranchLogo.FileName;
                            string filePath = Path.Combine(folder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await branch.BranchLogo.CopyToAsync(stream);
                            }

                            newBranch.BranchLogo = $"/MyFile/images/{fileName}";
                        }

                        

                        bank.Branches.Add(newBranch);
                    }

                }

                _context.Update(bank);
                await _context.SaveChangesAsync();

                isSuccess = true;
                message = "Bank and Branches Updated Successfully!";
            }

            return Json(new {success=$"{isSuccess}", message=$"{message}"});
        }


        [HttpGet]
        public IActionResult CreateBankWithBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBankWithBranch(CreateBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Bank and Branches Invalid Info!";
            
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);

                foreach (var item in errors)
                {
                    Console.WriteLine(item);
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
                    string fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "_" + viewModel.BankLogo.FileName;
                    string filePath = Path.Combine(folder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.BankLogo.CopyToAsync(fileStream);
                    }

                    bank.Logo = $"/MyFile/images/{fileName}";
                }

               


                foreach (var branch in viewModel.Branches)
                {
                    Branch newBranch = new Branch
                    {
                        BranchName = branch.BranchName,
                        IsActive = branch.IsActive,
                    };

                    if (branch.BranchLogo != null)
                    {
                        string folder = Path.Combine(Directory.GetCurrentDirectory(), "MyFile", "images");
                        if (Directory.Exists(folder) == false)
                        {
                            Directory.CreateDirectory(folder);
                        }
                        string fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff") + "_" + branch.BranchLogo.FileName;
                        string filePath = Path.Combine(folder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await branch.BranchLogo.CopyToAsync(fileStream);
                        }
                        newBranch.BranchLogo = $"/MyFile/images/{fileName}";
                    }

                    bank.Branches.Add(newBranch);

                }

                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank and Branches Added Successfully!";
            }

            return Json(new {success=$"{isSuccess}", message=$"{message}" });
        }

    }
}