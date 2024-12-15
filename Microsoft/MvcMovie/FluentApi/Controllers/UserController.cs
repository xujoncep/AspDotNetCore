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
        public async Task<IActionResult> EditUser(Guid id)
        {
            var user = await _context.Users.Include(a => a.Address).Include(p => p.Passport).FirstOrDefaultAsync(u =>u.UserId==id);

            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new EditUserVM
            {
               UserId = user.UserId,
               UserName=user.UserName,
               Birthdate=user.Birthdate,
               PassportNumber =user.Passport.PassportNumber,
               IsValid =user.Passport.IsValid,
               CityName =user.Address.CityName,
               SectionName =user.Address.SectionName,
               RoadNumber =user.Address.RoadNumber,
               ZipCode =user.Address.ZipCode,
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.Include(a => a.Address)
                                               .Include(p => p.Passport)
                                               .FirstOrDefaultAsync(u => u.UserId == viewModel.UserId);

                if (user == null)
                {
                    return NotFound();
                }


                user.UserName = viewModel.UserName;
                user.Birthdate = viewModel.Birthdate;
                user.Passport.PassportNumber = viewModel.PassportNumber;
                user.Passport.IsValid = viewModel.IsValid;
                user.Address.CityName = viewModel.CityName;
                user.Address.SectionName = viewModel.SectionName;
                user.Address.RoadNumber = viewModel.RoadNumber;
                user.Address.ZipCode = viewModel.ZipCode;
                
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);

        }


        [HttpGet]

        public async Task<IActionResult> DetailsUser(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var user = await _context.Users.Include(a => a.Address).Include(p => p.Passport).FirstOrDefaultAsync(u => u.UserId == id);
            
            if (user == null)
            {
                return NotFound();
            }

            var viewModel = new UserDetailsVM
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Birthdate = user.Birthdate,
                PassportNumber = user.Passport.PassportNumber,
                IsValid = user.Passport.IsValid,
                CityName = user.Address.CityName,
                SectionName = user.Address.SectionName,
                RoadNumber = user.Address.RoadNumber,
                ZipCode = user.Address.ZipCode,
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.Include(a => a.Address).Include(p => p.Passport).FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            var data = new DeleteUserVM
            { 
                UserId = user.UserId,
                UserName = user.UserName,
                PassportNumber = user.Passport.PassportNumber,
                CityName = user.Address.CityName,
                RoadNumber = user.Address.RoadNumber,               
            };
            return View(data);
        }


        [HttpPost]
        [ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteUserConfirm(Guid id)
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

        //duplicate id check in database
        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> IsPassportExists(string PassportNumber)
        {
            var data = await _context.Users.Include(p => p.Passport).Where(d => d.Passport.PassportNumber == PassportNumber).FirstOrDefaultAsync();

            if (data != null)
            {
                return Json($"UserId {PassportNumber} already taken!");
            }

            else
            {
                return Json(true);
            }


        }

    }
}
