using System.ComponentModel.DataAnnotations;

namespace StudentCrudWithViewModel.ViewModels
{
    public class StudentInfoJoinViewModel
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Class { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
    }
}
