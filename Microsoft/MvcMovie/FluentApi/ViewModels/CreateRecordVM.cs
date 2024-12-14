using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class CreateRecordVM
    {
        public Guid UserId { get; set; }

        [Required]
        [Remote(action: "IsUserNameExists", controller:"User")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Required]
        [Remote(action: "IsPassportExists", controller:"User")]
        [DisplayName("Passport Number")]
        public string? PassportNumber { get; set; }
        [Required]
        public bool? IsValid { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string? CityName { get; set; }
        
        [Required]
        [DisplayName("Section Name")]
        public string? SectionName { get; set; }
       
        [Required]
        [DisplayName("Road Number")]
        public int RoadNumber { get; set; }
        [Required]
        public string? ZipCode { get; set; }
    }
}
