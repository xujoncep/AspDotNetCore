using Microsoft.AspNetCore.Mvc;

namespace OBlog.Controllers
{
    public class AdminTagController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add()
        {
            return View();
        }
    }
}
