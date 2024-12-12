using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class Passport
    {
        [Key]
        public int PassportId { get; set; }
        [Required]
        public string? PassportNumber { get; set; }
        [Required]
        public bool? IsValid { get; set; }

        //[ForeignKey("UserId")]
        //public Guid UserId { get; set; }    
        public User? User { get; set; }

    }
}
