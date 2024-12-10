namespace FluentApi.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        public string? Name { get; set; }

        //Relationship: Many to Many

        public ICollection<StudentSubject>? StudentSubject { get; set; }
    }
}
