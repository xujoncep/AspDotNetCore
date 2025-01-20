using Microsoft.AspNetCore.Mvc;
using Pratices.Models;
using System.Diagnostics;

namespace Pratices.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }

        //Without DI we have to create object by new keyword which is tightly coupled
        public JsonResult IndexWithoutDI()
        {
            StudentRepository repository = new StudentRepository();
            List<Student> allStudentDetails = repository.GetAllStudent();
            return Json(allStudentDetails);
        }


        //With DI we will inject the serverce with contructor injection and also achive losely coupled design
        public JsonResult IndexWithDI()
        {
           
            var allStudentDetails = _studentRepository.GetAllStudent();
            return Json(allStudentDetails);
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
    }
}
