using Data_Passing_Techniques.Models;

namespace Data_Passing_Techniques.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student? Student { get; set; }
        public Address? Address { get; set; }

        public string? Title { get; set; }
    }
}
