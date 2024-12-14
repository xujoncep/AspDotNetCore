using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovieOneToMany.Data;
using MvcMovieOneToMany.Models;
using MvcMovieOneToMany.Models.ViewModel;

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
        public async Task<IActionResult> CreateGenre(CreateGenreViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    GenreName = viewModel.GenreName,
                };
                _dbcontext.Genres.Add(genre);
                await _dbcontext.SaveChangesAsync();
                TempData["SuccessMessage"] = " Genre Successfully Created!";
                return RedirectToAction(nameof(GenreList));

            }
            return View(viewModel);
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
                TempData["UpdateMessage"] = " Genre Successfully Updated!";
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
            TempData["DeleteMessage"] = " Genre Successfully Deleted!";
            return RedirectToAction(nameof(GenreList));
        }

       
        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> IsGenreExists(string GenreName)
        {
            var data = await _dbcontext.Genres.Where(g => g.GenreName==GenreName).FirstOrDefaultAsync();

            if (data != null)
            {
                return Json($"Genre {GenreName} already Exists!");
            }

            else
            {
                return Json(true);
            }


        }

    }
}
