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
        public IActionResult Privacy()
        {
            return View();
        }

      
        public IActionResult StudentList()
        {
            var data = _context.Students.ToList();
            return View(data);
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
                var data = new Student
                {
                    Name = txtName,
                    Email = txtEmail
                };

                _context.Students.Add(data);
                _context.SaveChanges();
                return RedirectToAction("Index","Students");

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
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index", "Students");
            }
            return View();
        }

        //fetch-then 
        [HttpGet]
        public IActionResult JavaScriptForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JavaScriptForm([FromBody] Student model)
        {
            bool success = false;
            string message = "Data not saved!";
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                success = true;
                message = "Data Saved";
            }

            return Json(new { success = $"{success}", message = $"{message}" });
        }


        //Axios
        [HttpGet]
        [Route("JavaScriptFormAxios")]
        public IActionResult JavaScriptFormAxios()
        {
            return View();
        }

        [HttpPost]
        public IActionResult JavaScriptFormAxios([FromBody] Student model)
        {
            bool success = false;
            string message = "Data not saved!";
            if (ModelState.IsValid)
            {
                _context.Add(model);
                _context.SaveChanges();
                success = true;
                message = "Data Saved";
            }

            return Json(new { success = $"{success}", message = $"{message}" });
        }


        //Ajax form Data

        [HttpGet]
        public IActionResult AjaxFormData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjaxFormData(Student model)
        {
            bool success = false;
            string message = "Data not saved!";
            if (ModelState.IsValid)
            {
               
                _context.Students.Add(model);
                _context.SaveChanges();
                success = true;
                message = "Data Saved";
               // return Json(new { success = true, message = "Data saved successfully!" });
            }
            //else
            //{
            //     success = false;
            //     message = "Data not Saved";
            //}

            return Json(new { success = $"{success}", message = $"{message}" });
        }


        //Ajax Form Serialization

        [HttpGet]
        public IActionResult AjaxFormSerializeData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjaxFormSerializeData([FromBody] Student model)
        {
            bool success = false;
            string message = "Data not saved!";
            if (ModelState.IsValid)
            {
                _context.Students.Add(model);
                _context.SaveChanges();
                success = true;
                message = "Data Saved";
            }

            return Json(new { success = $"{success}", message = $"{message}" });
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
