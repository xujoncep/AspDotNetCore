using FluentApi.Data;
using FluentApi.Models;
using FluentApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FluentApi.Controllers
{
    public class UserController : Controller
    {
        private readonly FluentApiDbContext _context;
        public UserController( FluentApiDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var info = await _context.Users.Include(a => a.Address).Include(p => p.Passport).ToListAsync();

            return View(info);
        }


        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();    
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateRecordVM viewModel)
        {
            
            if (ModelState.IsValid)
            {
               
                //var id = Guid.NewGuid();

                var passport = new Passport
                {
                    //UserId = id,
                    PassportNumber = viewModel.PassportNumber,
                    IsValid = viewModel.IsValid,
                };
                await _context.Passports.AddAsync(passport);
                await _context.SaveChangesAsync();

                var address = new Address
                {
                    //UserId=id,
                    CityName = viewModel.CityName,
                    SectionName = viewModel.SectionName,
                    RoadNumber = viewModel.RoadNumber,
                    ZipCode = viewModel.ZipCode,
                };

                await _context.Addresss.AddAsync(address);
                await _context.SaveChangesAsync();


                var user = new User
                {
                    //UserId= id,
                    UserName = viewModel.UserName,
                    Birthdate = viewModel.Birthdate,
                    PassportId =passport.PassportId,
                    AddressId = address.AddressId,
                    
                    //PassportId = _context.Passports
                    //    .Where(p => p.UserId == id)
                    //    .Select(p => p.PassportId)
                    //    .FirstOrDefault(),
                    
                    //AddressId = _context.Addresss
                    //    .Where(a => a.UserId == id)
                    //    .Select(a => a.AddressId)
                    //    .FirstOrDefault(),
                };

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
             
            }
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserVM
            {
               UserName=user.UserName,
               PassportId = user.PassportId,
               //PassportNumber = user.Passport.PassportNumber,
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var item = await _context.Users.FindAsync(viewModel.UserId);

                if (item == null)
                {
                    return NotFound();
                }
                item.UserName = viewModel.UserName;
                //item.Passport.PassportNumber= viewModel.PassportNumber;

                _context.Update(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);

        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(g => g.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            TempData["UserName"] = user.UserName;

            return View(user);
        }


        [HttpPost]
        [ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteUserConfirm(int id)
        {
            var u = await _context.Users.FindAsync(id);
           

            if (u != null)
            {
                _context.Users.Remove(u);
        
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //duplicate id check in database
        [AcceptVerbs("Post","Get")]
        public async Task<IActionResult> IsUserNameExists(string UserName)
        {
            var data = await _context.Users.Where(d => d.UserName == UserName).FirstOrDefaultAsync();

            if (data != null)
            {
                return Json($"UserId {UserName} already taken!");
            }

            else
            {
                return Json(true);
            }


        }

    }
}
