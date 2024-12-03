using dotnettuitorials.net.Models;
using dotnettuitorials.net.Repository;
using Microsoft.AspNetCore.Mvc;
namespace dotnettuitorials.net.Controllers
{
    public class HomeController: Controller
    {
        private readonly IStudentRepository? _repository = null;
        private readonly SomeOtherService? _someOtherService = null;
        //Initialize the variable through constructor
        public HomeController(IStudentRepository repository, SomeOtherService someOtherService)
        {
            _repository = repository;
            _someOtherService = someOtherService;
        }
        public JsonResult JsonIndex()
        {
            List<Student>? allStudentDetails = _repository?.GetAllStudents();
            _someOtherService?.SomeMethod();
            return Json(allStudentDetails);
        }
        public JsonResult GetStudentDetails(int Id)
        {
            Student? studentDetails = _repository?.GetStudentById(Id);
            _someOtherService?.SomeMethod();
            return Json(studentDetails);
        }
       public IActionResult Index()
        {
            return View();
        }
    }
}
    

