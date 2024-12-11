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
        public IActionResult Index()
        {
            var info =  ( from user in _context.Users
                         join passport in _context.Passports
                         on user.UserId equals passport.UserId
                         select new UserListVM
                         {
                             UserId = user.UserId,
                             UserName = user.UserName,
                             PassportId = passport.PassportId,
                             PassportNumber = passport.PassportNumber,
                             
                         }).ToList();
            
            return View(info);
        }

       
        [HttpGet]

        public IActionResult CreateUser()
        {
            return View();    
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserVM viewModel)
        {
            
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserId = viewModel.UserId,
                    UserName = viewModel.UserName,
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                var passport = new Passport
                { 
                    PassportId = viewModel.PassportId,
                    UserId = viewModel.UserId,
                    PassportNumber= viewModel.PassportNumber,               
                };
                await _context.Passports.AddAsync(passport);
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
               UserId=user.UserId,
               UserName=user.UserName,
               PassportId= user.Passport.PassportId,
               PassportNumber = user.Passport.PassportNumber,    
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(EditUserVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var item = await _context.Users.FindAsync(viewModel.UserId);

                if (item == null)
                {
                    return NotFound();
                }
                item.UserName = viewModel.UserName;
                item.Passport.PassportNumber= viewModel.PassportNumber;

                _context.Update(item);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);

        }


        [HttpGet]
        public async Task<IActionResult> DeleteUser(int? id)
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
        public async Task<IActionResult> IsIdTaken(string UserName)
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
