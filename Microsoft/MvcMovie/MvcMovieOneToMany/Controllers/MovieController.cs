using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovieOneToMany.Data;
using MvcMovieOneToMany.Models;

namespace MvcMovieOneToMany.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieDbContext _context;
        public MovieController( MovieDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> MovieList()
        {
            var movie = await _context.Movies.Include(m => m.Genre).ToListAsync();

            return View(movie);
        }

       
        [HttpGet]
        public IActionResult CreateMovie()
        {
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "GenreName");
            return View();
        }

       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(Movie movie)
        {
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "GenreName", movie.GenreId);
            if (!ModelState.IsValid)
            {
                await _context.AddAsync(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MovieList));
            }
           
            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> EditMovie(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie= await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "GenreName");
            return View(movie);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(Guid id, Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _context.Update(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MovieList));
            }
            ViewBag.Genres = new SelectList(_context.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteMovie(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.FirstOrDefaultAsync(g => g.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }
            //TempData["MovieName"] = movie.Title ;

            return View(movie);
        }


        [HttpPost]
        [ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteMovieConfirm(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MovieList));
        }
    }
}
