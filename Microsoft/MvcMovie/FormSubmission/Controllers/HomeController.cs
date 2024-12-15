using FormSubmission.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FormSubmission.Controllers
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

        [AcceptVerbs("Get","Post")]        
     
        public IActionResult WeaklyTypedForm(int txtId, string txtName, string txtEmail) 
        {
            ViewBag.txtId = txtId;
            ViewBag.txtName = txtName;
            ViewBag.txtEmail = txtEmail;
            return View();
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult StornglyTypedForm(Student student)
        {
            ViewBag.txtId = student.Id;
            ViewBag.txtName = student.Name;
            ViewBag.txtEmail = student.Email;
            return View();
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult AjaxTypedForm()
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
