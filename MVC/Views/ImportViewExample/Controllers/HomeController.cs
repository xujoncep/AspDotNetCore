using ImportViewExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImportViewExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            List<Student> students = new List<Student>()
            {
                new Student(){StudentId=1001, Name="Jhon", Branch="Asia", Section="Day", Gender="Male"},
                new Student(){StudentId=1002, Name="Alex", Branch="Europe", Section="Day", Gender="Male"},
                new Student(){StudentId=1003, Name="Siri", Branch="America", Section="Day", Gender="Female"},
            };

            return View(students);
        }

        public ViewResult Details(int Id)
        {
            var student = new Student()
            {StudentId=1003, Name="Siri", Branch="America", Section="Day", Gender="Female"};

            return View(student);
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
