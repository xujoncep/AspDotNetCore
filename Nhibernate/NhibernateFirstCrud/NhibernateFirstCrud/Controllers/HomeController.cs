using Microsoft.AspNetCore.Mvc;
using NhibernateFirstCrud.Models;
using System.Diagnostics;

namespace NhibernateFirstCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly NHibernate.ISession _session;

        public HomeController(ILogger<HomeController> logger, NHibernate.ISession session)
        {
            _logger = logger;
            _session = session;
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
