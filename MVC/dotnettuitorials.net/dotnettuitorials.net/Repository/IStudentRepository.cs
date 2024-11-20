using dotnettuitorials.net.Models;

namespace dotnettuitorials.net.Repository
{
    public interface IStudentRepository
    {
        Student GetStudentById(int StudentId);
        List<Student> GetAllStudents();
    }
}
