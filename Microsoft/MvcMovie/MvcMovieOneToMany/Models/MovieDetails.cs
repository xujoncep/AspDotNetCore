using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovieOneToMany.Models
{
    public class MovieDetails
    {
        [Key]
        public Guid MovieDetailsId { get; set; }

       
        public DateTime ReleaseDate { get; set; }

        
        public decimal Income { get; set; }

        public string Rating { get; set; }

    }
}
