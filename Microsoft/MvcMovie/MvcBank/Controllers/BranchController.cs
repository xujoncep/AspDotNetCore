using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBank.Data;
using MvcBank.Models;
using MvcBank.Models.ViewModels;

namespace MvcBank.Controllers
{
    public class BranchController : Controller
    {

        private readonly BankDbContext _context;
        public BranchController(BankDbContext context)
        {
            _context = context;

        }
        
        [HttpGet]
        public async Task<IActionResult> BranchList()
        {
            var branch = await _context.Branch.Include(m => m.Bank).ToListAsync();

            return View(branch);
        }

       
        [HttpGet]
        public IActionResult CreateBranch()
        {
            var viewModel = new CreateBranchVM
            {
                Banks = _context.Banks.Select(b => new SelectListItem
                {
                    Value = b.BankId.ToString(),
                    Text = b.BankName
                })
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBranch(CreateBranchVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = new Branch
                {
                    BranchId = Guid.NewGuid(),
                    BranchName = viewModel.BranchName,
                    BankId = viewModel.BankId
                };
                await _context.AddAsync(branch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Branch successfully created!";
                return RedirectToAction(nameof(BranchList));
            }

            viewModel.Banks = _context.Banks.Select(g => new SelectListItem
            {
                Value = g.BankId.ToString(),
                Text = g.BankName
            });

            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditBranch(Guid id)
        {
            var branch = await _context.Branch.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new EditBranchVM
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                BankId = branch.BankId,
                Banks = _context.Banks.Select(b => new SelectListItem
                {
                    Value = b.BankId.ToString(),
                    Text = b.BankName
                })
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBranch(EditBranchVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = await _context.Branch.FindAsync(viewModel.BranchId);

                if (branch == null)
                {
                    return NotFound();
                }

                branch.BranchName = viewModel.BranchName;
                branch.BankId = viewModel.BankId;

                _context.Update(branch);
                await _context.SaveChangesAsync();
                TempData["UpdateMessage"] = "Branch successfully Updated!";
                return RedirectToAction(nameof(BranchList));
            }

            viewModel.Banks = _context.Banks.Select(g => new SelectListItem
            {
                Value = g.BankId.ToString(),
                Text = g.BankName
            });

            return View(viewModel);

        }


        [HttpGet]
        public async Task<IActionResult> DeleteBranch(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var branch = await _context.Branch.Include(b => b.Bank).FirstOrDefaultAsync(b =>b.BranchId == id);

            if (branch == null)
            {
                return NotFound();
            }

            TempData["BranchName"] = branch.BranchName;

            return View(branch);
        }


        [HttpPost]
        [ActionName("DeleteBranch")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteBranchConfirm(Guid id)
        {
            var branch = await _context.Branch.FindAsync(id);

            if (branch != null)
            {
                _context.Branch.Remove(branch);
            }
            await _context.SaveChangesAsync();
            TempData["DeleteMessage"] = "Branch successfully Deleted!";
            return RedirectToAction(nameof(BranchList));
        }

        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> IsBranchExists(string BranchName)
        {
            var data = await _context.Branch.Where(b => b.BranchName == BranchName).FirstOrDefaultAsync();

            if (data != null)
            {
                return Json($"Branch {BranchName} already in Website!");
            }

            else
            {
                return Json(true);
            }


        }
    }
}
