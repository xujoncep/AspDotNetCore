using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class EditUserVM
    {
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        public int PassportId { get; set; }
        public string PassportNumber { get; set; }
    }
}
