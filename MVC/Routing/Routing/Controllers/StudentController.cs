using Microsoft.AspNetCore.Mvc;
using Routing.Models;

namespace Routing.Controllers
{
    public class StudentController : Controller
    {
        static List<Student> students = new List<Student>()
        {
            new Student() { Id = 1, Name = "Pranaya" },
            new Student() { Id = 2, Name = "Priyanka" },
            new Student() { Id = 3, Name = "Anurag" },
            new Student() { Id = 4, Name = "Sambit" }
        };

        [Route("Student/All")]
        public List<Student> GetAllStudents()
        {
            return students;
        }

        [Route("Student/{studentID}/Details")]
        public Student GetStudentByID(int studentID)
        {
            
            Student? studentDetails = students.FirstOrDefault(s => s.Id == studentID);
            return studentDetails ?? new Student();
        }

        [Route("Student/{studentID}/Courses")]
        public List<string> GetStudentCourses(int studentID)
        {
           
            List<string> CourseList = new List<string>();
            if (studentID == 1)
                CourseList = new List<string>() { "ASP.NET Core", "C#.NET", "SQL Server" };
            else if (studentID == 2)
                CourseList = new List<string>() { "ASP.NET Core MVC", "C#.NET", "ADO.NET Core" };
            else if (studentID == 3)
                CourseList = new List<string>() { "ASP.NET Core WEB API", "C#.NET", "Entity Framework Core" };
            else
                CourseList = new List<string>() { "Bootstrap", "jQuery", "AngularJs" };

            return CourseList;
        }
    }
}
