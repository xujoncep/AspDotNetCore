using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class UserDetailsVM
    {

        public Guid UserId { get; set; }

        [Required]
        [Remote(action: "IsUserNameExists", controller: "User")]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Required]
        [Remote(action: "IsPassportExists", controller: "User")]
        public string? PassportNumber { get; set; }
        [Required]
        public bool? IsValid { get; set; }

        [Required]
        public string? CityName { get; set; }

        [Required]
        public string? SectionName { get; set; }

        [Required]
        public int RoadNumber { get; set; }
        [Required]
        public string? ZipCode { get; set; }
    }
}
