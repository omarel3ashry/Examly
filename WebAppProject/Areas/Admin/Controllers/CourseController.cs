using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace WebAppProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class CourseController : Controller
    {

        private readonly ICourseRepository courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            List<Course> model = courseRepository.GetAll();
            return View(model);
        }
        public IActionResult Details(int Id)
        {
            Course? model = courseRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course Course)
        {

            int res = courseRepository.Add(Course);
            IActionResult actionResult = res > 0 ? RedirectToAction("Index") : RedirectToAction("Create");
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            Course? model = courseRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Edit(Course Course)
        {
            bool res = courseRepository.Update(Course);
            IActionResult actionResult = res ? RedirectToAction("Index") : RedirectToAction("Edit");
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {

            Course? model = courseRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Delete(Course Course)
        {
            Debug.WriteLine(Course.Id);
            courseRepository.Delete(Course.Id);
            return RedirectToAction("Index");
        }


    }
}
