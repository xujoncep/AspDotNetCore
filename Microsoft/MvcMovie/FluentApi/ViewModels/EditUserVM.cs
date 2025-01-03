﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class EditUserVM
    {
        [DisplayName("User Identity Number")]
        public Guid UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Required]
        [DisplayName("Passport Number")]
        public string? PassportNumber { get; set; }
       
        [Required]
        [DisplayName("Passport Validity")]
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
        [DisplayName("Zip Code")]
        public string? ZipCode { get; set; }
    }
}
