using Data_Passing_Techniques.Models;
using Data_Passing_Techniques.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Data_Passing_Techniques.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ViewResult StudentDetails()
        {
            Student student = new Student()
            {
                StudentId = 1003,
                Name = "Jhon",
                Branch = "Office",
                Gender ="Male"
            };

            Address address = new Address()
            {
                StudentId = 1003,
                City = "Dhaka",
                Country = "BD",
                ZipCode = "6290"
            };

            StudentDetailsViewModel studentDetailsViewModel = new StudentDetailsViewModel()
            {
                Student = student,
                Address = address,
                Title = "Details with view model",

            };


            return View(studentDetailsViewModel);
        }
    }
}
