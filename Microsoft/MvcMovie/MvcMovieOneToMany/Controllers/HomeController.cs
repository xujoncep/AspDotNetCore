using Microsoft.AspNetCore.Mvc;
using MvcMovieOneToMany.Data;
using MvcMovieOneToMany.Models;
using System.Diagnostics;

namespace MvcMovieOneToMany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger )
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
    }
}
