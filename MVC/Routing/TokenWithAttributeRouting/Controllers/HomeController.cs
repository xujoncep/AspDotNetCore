using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TokenWithAttributeRouting.Models;

namespace TokenWithAttributeRouting.Controllers
{
   // [Route("[controller]/[action]")] //Using controller and action tokens at the Controller level using the Route Attribute
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("/")]   //this is default routing
        [Route("[action]")]
        public string Index()
        {
            return "Index() Action Method of HomeController";
        }
        
        [Route("[action]")]
        public string Details()
        {
            return "Details() Action Method of HomeController";
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
