using Microsoft.AspNetCore.Mvc;

namespace MvcMovieOneToMany.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult MovieList()
        {
            return View();
        }
    }
}
