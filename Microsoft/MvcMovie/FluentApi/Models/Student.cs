namespace FluentApi.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string? Name { get; set; }

        // Relationship: many to many

        public ICollection<StudentSubject>? StudentSubject { get; set; }
    }
}
