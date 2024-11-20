using Microsoft.AspNetCore.Mvc;

namespace Data_Passing_Techniques.Controllers
{
    public class TempController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HomeFirst()
        {
            TempData["Name"] = "Pranaya";
            TempData["Age"] = 30;
            TempData.Keep("Name");
            TempData.Keep("Age");
            return View();
        }

        public IActionResult HomeSecond()
        {
            TempData.Keep("Name");
            TempData.Keep("Age");

            return View();
        }

        public IActionResult HomeThired()
        {
          
            return View();
        }
    }
}
