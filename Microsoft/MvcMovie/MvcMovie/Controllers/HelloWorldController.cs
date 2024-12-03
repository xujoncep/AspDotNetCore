using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        //get ~/HelloWorld
        //public string Index()
        //{
        //    return "This is a index page of application!";
        //}

        public IActionResult Index()
        {
            return View();
        }

        //get ~/HelloWorld/Welcome
        public IActionResult Welcome(string name, int num=1)
        {
            //return "Welcome to asp dot net core!";

            //return $" Hello {name}! Looping time is {num}";

            ViewData["name"] = "Hello "+ name;
            ViewData["number"] = num;

            return View();
        }
    }
}
