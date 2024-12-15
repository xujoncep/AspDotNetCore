using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcBank.Data;
using MvcBank.Models;
using MvcBank.Models.ViewModels;

namespace MvcBank.Controllers
{
    public class BankController : Controller
    {
        private readonly BankDbContext _context;
        public BankController(BankDbContext context)
        {
            _context = context;
            
        }

        [HttpGet]
        public async Task<IActionResult> BankList()
        {
            var banks = await _context.Banks.ToListAsync();

            return View(banks);
        }


        [HttpGet]
        public IActionResult CreateBank()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBank(CreateBankVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var bank = new Bank
                {
                    BankName = viewModel.BankName,
                };
                _context.Banks.Add(bank);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = " Bank Successfully Created!";
                return RedirectToAction(nameof(BankList));

            }
            return View(viewModel);
        }


        [HttpGet]
        public async Task<ActionResult> EditBank(Guid? id)
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

            return View(bank);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBank(Guid id, Bank bank)
        {
            if (id != bank.BankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(bank);
                await _context.SaveChangesAsync();
                TempData["UpdateMessage"] = " Bank Successfully Updated!";
                return RedirectToAction(nameof(BankList));
            }
            return View(bank);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteBank(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bank = await _context.Banks.FirstOrDefaultAsync(b => b.BankId == id);

            if (bank == null)
            {
                return NotFound();
            }

            TempData["BankName"] = bank.BankName;

            return View(bank);
        }


        [HttpPost]
        [ActionName("DeleteBank")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteBankConfirm(Guid id)
        {
            var bank = await _context.Banks.FindAsync(id);

            if (bank != null)
            {
                _context.Banks.Remove(bank);
            }
            await _context.SaveChangesAsync();
            TempData["DeleteMessage"] = " Bank Successfully Deleted!";
            return RedirectToAction(nameof(BankList));
        }







        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> IsBankExists(string BankName)
        {
            var data = await _context.Banks.Where(b => b.BankName == BankName).FirstOrDefaultAsync();

            if (data != null)
            {
                //return BadRequest($"Bank {BankName} already Exists!");
                return Json($"Bank {BankName} already Exists!");
            }

            else
            {
                return Json(true);
                //return Ok(true);
            }


        }
    }
}
