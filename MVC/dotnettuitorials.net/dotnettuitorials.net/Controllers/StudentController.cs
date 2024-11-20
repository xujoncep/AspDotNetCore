using dotnettuitorials.net.Repository;
using Microsoft.AspNetCore.Mvc;

namespace dotnettuitorials.net.Controllers
{
    public class StudentController : Controller
    {
        public JsonResult GetAllStudents(int StudentId)
        {
            StudentRepository repository = new StudentRepository();
            Models.Student studentDetails = repository.GetStudentById(StudentId);
            return Json(studentDetails);
        }
    }
}
