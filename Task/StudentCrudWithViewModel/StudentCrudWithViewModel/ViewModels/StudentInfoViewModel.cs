using StudentCrudWithViewModel.Models;

namespace StudentCrudWithViewModel.ViewModels
{
    public class StudentInfoViewModel
    {
        public List<Student> Students { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
