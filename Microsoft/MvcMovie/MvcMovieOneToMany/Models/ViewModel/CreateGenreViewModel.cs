using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcMovieOneToMany.Models.ViewModel
{
    public class CreateGenreViewModel
    {
        public Guid GenreId { get; set; }

        [Required]
        [Remote(action: "IsGenreExists", controller: "Genre")]
        public string GenreName { get; set; }

        //public ICollection<Movie>? Movies { get; set; }
        //public List<Movie>? Movies { get;set; }
        public IEnumerable<Movie>? Movies { get; set; }
    }
}
