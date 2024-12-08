using System.ComponentModel.DataAnnotations;

namespace MvcMovieOneToMany.Models
{
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }

        public string Title { get; set; }

        public Guid GenreId { get; set; }

        public Guid MovieDetailsId { get; set; }
        public Genre Genre { get; set; }
        public MovieDetails MovieDetails { get; set; }
    }
}
