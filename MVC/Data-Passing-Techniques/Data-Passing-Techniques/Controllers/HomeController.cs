using Data_Passing_Techniques.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Data_Passing_Techniques.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        public ViewResult ViewDataDetails()
        {
            ViewData["Title"] = "Details Page";

            Student student = new Student()
            {
                StudentId =1001,
                Name ="Alice",
                Branch ="Head Branch",
                Gender = "Female"
            };
            ViewData["Student"] = student;
            return View();
        }

        public ViewResult ViewBagDetails()
        {
            ViewBag.Title = "Details Page";

            Student student = new Student()
            {
                StudentId = 1001,
                Name = "Alice",
                Branch = "Head Branch",
                Gender = "Female"
            };
            ViewBag.Student = student;
            return View();
        }

        public ViewResult StorngTypeDetails()
        {
            ViewBag.Title = "Details Page";

            Student student = new Student()
            {
                StudentId = 1002,
                Name = "Bob",
                Branch = "Head Branch",
                Gender = "Female"
            };
            return View(student);
        }
    }
}
