using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovieOneToMany.Data;
using MvcMovieOneToMany.Models;

namespace MvcMovieOneToMany.Controllers
{
    public class GenreController : Controller
    {
        private readonly MovieDbContext _dbcontext;
        public GenreController(MovieDbContext context)
        {
            _dbcontext = context;
        }
       
        public async Task<IActionResult> GenreList(Genre model)
        {
            var genres = await _dbcontext.Genres.ToListAsync();

            return View(genres);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]  
        public async Task<IActionResult> CreateGenre()
        {
            //if (!ModelState.IsValid)
            //{
            //    return NotFound();
            //}
            return View();
        }

    }
}
