using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MvcMovieOneToMany.Models.ViewModel
{
    public class CreateMovieViewModel
    {
        public Guid MovieId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Guid? GenreId { get; set; }

        public IEnumerable<SelectListItem>? Genres { get; set; }
    }

}
