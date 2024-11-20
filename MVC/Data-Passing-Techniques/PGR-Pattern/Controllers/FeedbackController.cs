using Microsoft.AspNetCore.Mvc;
using PGR_Pattern.Models;

namespace PGR_Pattern.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                TempData["SuccessMessage"] = "Thank you for your feedback";
                return RedirectToAction("Confirmation");
            }
            return View(feedback);
        }

        [HttpGet]
        public IActionResult Confirmation() 
        {

            return View();

        }
    }
}
