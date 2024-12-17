using FormSubmission.Data;
using FormSubmission.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FormSubmission.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FormDbContext _context;

        public HomeController(ILogger<HomeController> logger, FormDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult StudentList()
        {
            var data = _context.Students.ToList();
            return View(data);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
        
        [HttpGet]
        public IActionResult WeaklyTypedForm()
        {
            return View();
        }


        [HttpPost]
        public IActionResult WeaklyTypedForm(string txtName, string txtEmail) 
        {
            if(ModelState.IsValid)
            {
                var data = new Student();
                data.Name = txtName;
                data.Email = txtEmail;

                _context.Students.Add(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(StudentList));

            }
            return View();
        }
        
        [HttpGet]
        public IActionResult StornglyTypedForm()
        {

            return View();
        }

        [HttpPost]
        public IActionResult StornglyTypedForm(Student student)
        {
           
            if(ModelState.IsValid)
            {
                //var data = new Student();
                //data.Name=student.Name;
                //data.Email=student.Email;
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(StudentList));
            }
            return View();
        }

        public IActionResult JavaScriptForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JavaScriptForm([FromBody] Student model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                //return Json(new { success = true, message = "Student data saved successfully!" });

                TempData["SuccessMessage"] = "Student data saved successfully!";
                return RedirectToAction(nameof(StudentList));
            }

            //return Json(new { success = false, message = "Invalid data. Please check your input." });
            
            TempData["ErrorMessage"] = "Invalid data. Please check your input.";
            return View();
        }

        [HttpGet]
        public IActionResult AjaxFormData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjaxFormData(Student model)
        {
            if (ModelState.IsValid)
            {
               
                _context.Students.Add(model);
                _context.SaveChanges();
                return Json(new { success = true, message = "Student data saved successfully!" });
            }

            return Json(new { success = false, message = "Invalid data. Please check your input." });
        }



        [HttpGet]
        public IActionResult AjaxFormSerializeData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjaxFormSerializeData([FromBody] Student model)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(model);
                _context.SaveChanges();
                return Json(new { success = true, message = "Student saved successfully!" });
            }

            return Json(new { success = false, message = "Failed to save student data." });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
