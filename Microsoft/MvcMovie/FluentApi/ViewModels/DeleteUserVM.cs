using System.ComponentModel.DataAnnotations;

namespace FluentApi.ViewModels
{
    public class DeleteUserVM
    {
        public Guid UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? PassportNumber { get; set; }
       

        [Required]
        public string? CityName { get; set; }

        [Required]
        public int RoadNumber { get; set; }

    }
}
