using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluentApi.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

       //Passport section

        [ForeignKey("PassportId")]
        public int PassportId { get; set; }
        public Passport Passport { get; set; }

        //Address section

        [ForeignKey("AddressId")]
        public int AddressId { get; set; }

        public Address Address { get; set; }


    }
}
