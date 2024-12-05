using Microsoft.AspNetCore.Mvc;
using OBlog.Data;
using OBlog.Models.Domain;
using OBlog.Models.ViewModels;

namespace OBlog.Controllers
{
    public class AdminTagController : Controller
    {
        private readonly BlogDbContext _context;
        public AdminTagController(BlogDbContext blogDbContext)
        {
            _context=blogDbContext;  
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddTag(AddTagRequest addTagRequest)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            var tags = _context.Tags.ToList();

            return View(tags);
        }
    }
}
