using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovieOneToMany.Data;
using MvcMovieOneToMany.Models;
using MvcMovieOneToMany.Models.ViewModel;

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
            var viewModel = new CreateMovieViewModel
            {
                Genres = _context.Genres.Select(g => new SelectListItem
                {
                    Value = g.GenreId.ToString(),
                    Text = g.GenreName
                })
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMovie(CreateMovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    MovieId = Guid.NewGuid(),
                    Title = viewModel.Title,
                    GenreId = viewModel.GenreId
                };

                await _context.AddAsync(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MovieList));
            }

            viewModel.Genres = _context.Genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            });

            return View(viewModel);
        }



        [HttpGet]
        public async Task<IActionResult> EditMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var viewModel = new EditMovieViewModel
            {
                MovieId = movie.MovieId,
                Title = movie.Title,
                GenreId = movie.GenreId,
                Genres = _context.Genres.Select(g => new SelectListItem
                {
                    Value = g.GenreId.ToString(),
                    Text = g.GenreName
                })
            };

            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(EditMovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = await _context.Movies.FindAsync(viewModel.MovieId);

                if (movie == null)
                {
                    return NotFound();
                }

                movie.Title = viewModel.Title;
                movie.GenreId = viewModel.GenreId;

                _context.Update(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MovieList));
            }

            viewModel.Genres = _context.Genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            });
            
            return View(viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteMovie(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = await _context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(g => g.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }
            
            TempData["MovieName"] = movie.Title ;

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
