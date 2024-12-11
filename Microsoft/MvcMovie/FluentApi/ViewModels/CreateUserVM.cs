using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class CreateUserVM
    {
        public int UserId { get; set; }
        
        [Remote(action: "IsIdTaken", controller: "User")]
        [Required]
        public string? UserName { get; set; }
        public int PassportId { get; set; }
        public int PassportNumber { get; set; }
    }
}
