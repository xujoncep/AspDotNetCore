namespace DifferentForm.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
