using BankApplication.Data;
using BankApplication.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApplication.Controllers
{
    public class BankWithBranchController : Controller
    {
        private readonly BankDbContext _context;

        public BankWithBranchController(BankDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var banks = await _context.Banks.Include(b => b.Branches).ToListAsync();
            return View(banks);
        }


   
        public IActionResult Create()
        {
            return View(new Bank { Branches = new List<Branch> { new Branch() } });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bank bank, List<IFormFile> branchLogos)
        {
            if (ModelState.IsValid)
            {
                // Save Bank Logo if provided
                if (Request.Form.Files["BankLogo"] != null)
                {
                    var file = Request.Form.Files["BankLogo"];
                    string bankLogoPath = Path.Combine("wwwroot/images", Guid.NewGuid() + "_" + file.FileName);
                    using (var stream = new FileStream(bankLogoPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    bank.Logo = bankLogoPath.Replace("wwwroot", ""); // Relative path for the database
                }

                // Handle Branch Logos
                for (int i = 0; i < bank.Branches.Count; i++)
                {
                    var branch = bank.Branches[i];

                    if (branchLogos.Count > i && branchLogos[i] != null)
                    {
                        var branchLogoPath = Path.Combine("wwwroot/images", Guid.NewGuid() + "_" + branchLogos[i].FileName);
                        using (var stream = new FileStream(branchLogoPath, FileMode.Create))
                        {
                            await branchLogos[i].CopyToAsync(stream);
                        }
                        branch.BranchLogo = branchLogoPath.Replace("wwwroot", ""); // Relative path for the database
                    }
                }

                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bank);
        }

    }
}
