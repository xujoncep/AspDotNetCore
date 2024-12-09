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

        [HttpGet]
        public async Task<IActionResult> GenreList(Genre model)
        {
            var genres = await _dbcontext.Genres.ToListAsync();

            return View(genres);
        }

        
        [HttpGet]
        public IActionResult CreateGenre()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Genres.Add(genre);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(GenreList));

            }
            return View(genre);
        }

        [HttpGet]
        public async Task<ActionResult> EditGenre(Guid? id)
        {
            if(id ==null)
            {
                return NotFound();
            }
            var genre = await _dbcontext.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGenre(Guid id, Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _dbcontext.Update(genre);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(GenreList));
            }
            return View(genre);
        }


        [HttpGet]
        public async Task<IActionResult> DeleteGenre(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var genre = await _dbcontext.Genres.FirstOrDefaultAsync(g => g.GenreId == id);
            
            if(genre == null)
            {
                return NotFound();
            }
            TempData["GenreName"] = genre.GenreName;

            return View(genre);
        }

        
        [HttpPost]
        [ActionName("DeleteGenre")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteGenreConfirm(Guid id)
        {
            var genre = await _dbcontext.Genres.FindAsync(id);

            if(genre != null)
            {
                _dbcontext.Genres.Remove(genre);
            }
            await _dbcontext.SaveChangesAsync();

            return RedirectToAction(nameof(GenreList));
        }

    }
}
