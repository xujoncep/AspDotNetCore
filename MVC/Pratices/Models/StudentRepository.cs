namespace Pratices.Models
{
    public class StudentRepository: IStudentRepository
    {
        public List<Student> Data()
        {

            return new List<Student>()
            { 
              new Student () {StudentId = 1, Name = "jhon"},
              new Student () {StudentId = 1, Name = "jhon"}

            };
        }

        public List<Student> GetAllStudent()
        {
            return Data();
        }
    }
}
