using BankApplication.Data;
using BankApplication.Models.Entity;
using BankApplication.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BankApplication.Controllers
{
    public class BankV4Controller : Controller
    {
        private readonly BankDbContext _context;

        public BankV4Controller( BankDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var banks = await _context.Banks.ToListAsync();
            return View(banks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create( CreateBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Bank information is invalid!";
            if (ModelState.IsValid == false)
            {

                var errors = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);

                foreach(var error in errors)
                {
                    Console.WriteLine(error);
                }  
            }

            else
            {

                var bank = new Bank
                {
                    BankName = viewModel.Bank.BankName,
                    BankAddress = viewModel.Bank.BankAddress,
                    Branches = new List<Branch>(),

                };

                if (viewModel.BankLogo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.BankLogo.CopyToAsync(memoryStream);
                        byte[] logo = memoryStream.ToArray();
                        bank.Logo = Convert.ToBase64String(logo);
                    }
                }

                foreach (var branch in viewModel.Branches)
                {
                    var branchVM = new Branch
                    {
                        BranchName = branch.BranchName,
                        IsActive = branch.IsActive,
                    };

                   if (branch.BranchLogo != null)
                   {
                        using (var memoryStream = new MemoryStream())
                        {
                            await branch.BranchLogo.CopyToAsync(memoryStream);
                            byte[] logo = memoryStream.ToArray();
                            branchVM.BranchLogo = Convert.ToBase64String(logo);
                        }
                   }

                    bank.Branches.Add(branchVM);
                }

                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                isSuccess = true;
                message = "Bank information is saved successfully!";
            }

            return Json( new { success=$"{isSuccess}", message=$"{message}"});
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
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
        public async Task<IActionResult> Edit(int id, EditBankWithMultipleBranchVM viewModel)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted!";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var errorMsg = "";
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                    errorMsg += error + "\n";
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
                    using (var memoryStream = new MemoryStream())
                    {
                        await viewModel.BankLogo.CopyToAsync(memoryStream);
                        byte[] logo = memoryStream.ToArray();
                        bank.Logo = Convert.ToBase64String(logo);
                    }
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
                            using (var memoryStream = new MemoryStream())
                            {
                                await branchVM.BranchLogo.CopyToAsync(memoryStream);
                                byte[] logo = memoryStream.ToArray();
                                existingBranch.BranchLogo = Convert.ToBase64String(logo);
                            }
                        }
                    }

                    else
                    {
                        string branchLogo = null;

                        if (branchVM.BranchLogo != null)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await branchVM.BranchLogo.CopyToAsync(memoryStream);
                                byte[] logo = memoryStream.ToArray();
                                branchLogo = Convert.ToBase64String(logo);
                            }

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

            return Json(new { success = $"{isSuccess}", message = $"{message}" });
        }
    }
}
