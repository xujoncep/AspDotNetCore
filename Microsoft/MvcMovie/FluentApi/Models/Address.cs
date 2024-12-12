using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? SectionName { get; set; }
        [Required]
        public int RoadNumber { get; set; }
        [Required]
        public string? ZipCode { get; set; }
       
        
        //[ForeignKey("UserId")]
        //public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
