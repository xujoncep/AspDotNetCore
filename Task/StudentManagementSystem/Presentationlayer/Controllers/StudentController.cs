using BusinessLogicLayer.IService;
using DataAccessLayer.Entites;
using DataAccessLayer.ViewModels;
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
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetStudentsAsync();
            var studentViewModels = students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Class = s.Class,
            }).ToList();
            return View(studentViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
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
                
                };

                await _studentService.AddStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class,
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
                    Class = studentViewModel.Class
                };
                await _studentService.UpdateStudentAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            var studentViewModel = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Class = student.Class
            };
            return View(studentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
