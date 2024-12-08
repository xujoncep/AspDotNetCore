using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MvcMovieOneToMany.Models.ViewModel
{
    public class FullMovieInfoViewModel
    {
        public Guid MovieId { get; set; }

        public string Title { get; set; } 

        public string Genre { get; set; }

        [DisplayName("Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Income { get; set; }

        public string Rating { get; set; }
    }
}
