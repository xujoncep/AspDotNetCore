using BusinessLogicLayer.IService;
using DataAccessLayer.Entites;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentationlayer.Controllers
{
    public class StudentController : Controller
    {

        private readonly IStudentService _studentService;

        public StudentController( IStudentService studentService)
        {
            _studentService = studentService;  
        }

        public IActionResult Data()
        {
            TempData["message"] = "This is tempdata";
            TempData.Keep();
            return View();
        }
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetStudentsAsync();
            var studentViewModels = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Class = s.Class,
                Email = s.Email,
                Phone = s.Phone,
                Bio = s.Bio,
            }).ToList();
            return View(studentViewModels);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
                Email = student.Email,
                Phone = student.Phone,
                Bio = student.Bio
            };
            return View(studentViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student 
                {               
                     Name= studentViewModel.Name,
                     Class = studentViewModel.Class,
                     Email = studentViewModel.Email,
                     Phone = studentViewModel.Phone,
                     Bio = studentViewModel.Bio,              
                };

                await _studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
                Email=student.Email,
                Phone = student.Phone,
                Bio = student.Bio,
            };

            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Id = studentViewModel.Id,
                    Name = studentViewModel.Name,
                    Class = studentViewModel.Class,
                    Email = studentViewModel.Email,
                    Phone = studentViewModel.Phone,
                    Bio = studentViewModel.Bio
                };
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
                Email = student.Email,
                Phone = student.Phone,
                Bio = student.Bio
            };
            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
