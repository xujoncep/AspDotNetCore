using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCrudWithViewModel.Data;
using StudentCrudWithViewModel.Models;
using StudentCrudWithViewModel.ViewModels;
using System.Diagnostics;

namespace StudentCrudWithViewModel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        //this is for StudentList
        public IActionResult StudentList()
        {
            var students= _context.Students.ToList();
            return View(students);
        }

        //this is for AddressList

        public IActionResult AddressList()
        {
            var addresses = _context.Addresses.ToList();
            return View(addresses);
        }

        //this is for full Information of student
        public IActionResult StudentFullInfo()
        {
            // V1- simple format

            //var info = new StudentInfoViewModel();
            //info.Students = _context.Students.ToList();
            //info.Addresses = _context.Addresses.ToList();

            // V2- Raw Query Format

            var info = new StudentInfoViewModel();
            info.Students = _context.Students.FromSqlRaw("Select * from Students").ToList();
            info.Addresses = _context.Addresses.FromSqlRaw("Select * from Addresses").ToList();

            // V3- Join table using view model
            //var info = ( from student in _context.Students
            //             join address in _context.Addresses
            //             on student.AddressId equals address.AddressId
            //             select new StudentInfoJoinViewModel
            //             { 
            //                 StudentId = student.StudentId,
            //                 Name = student.Name,
            //                 Age = student.Age,
            //                 Class = student.Class,
            //                 City = address.City,
            //                 Region = address.Region,                         
            //             });

            return View(info);
        }

        public IActionResult StudentInfoWithJoin()
        {
            // V3- Join table using view model
            var info = (from student in _context.Students
                        join address in _context.Addresses
                        on student.AddressId equals address.AddressId
                        select new StudentInfoJoinViewModel
                        {
                            StudentId = student.StudentId,
                            Name = student.Name,
                            Age = student.Age,
                            Class = student.Class,
                            City = address.City,
                            Region = address.Region,
                        }).ToList();

            return View(info);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
