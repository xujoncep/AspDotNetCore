using FluentApi.Data;
using FluentApi.Models;
using FluentApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

    }
}
