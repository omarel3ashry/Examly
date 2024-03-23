using AutoMapper;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppProject.Areas.Admin.ViewModels;



namespace WebAppProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class CourseController : Controller
    {

        private readonly ICourseRepository courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            this.courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<Course> courses = await courseRepository.GetAllAsync();
            var model = _mapper.Map<IEnumerable<CourseViewModel>>(courses);
            return View(model);
        }
        public async Task<IActionResult> Details(int Id)
        {
            IActionResult actionResult;
            Course? course = await courseRepository.GetByIdAsync(Id);
            if (course != null)
            {
                var model = _mapper.Map<CourseViewModel>(course);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(model);
                int res = await courseRepository.AddAsync(course);
                actionResult = res > 0 ? RedirectToAction("Index") : RedirectToAction("Create");
            }
            else
            {
                actionResult = View(model);
            }
            return actionResult;
        }

        public async Task<IActionResult> Edit(int Id)
        {
            IActionResult actionResult;
            Course? course = await courseRepository.GetByIdAsync(Id);
            if (course != null)
            {
                var model = _mapper.Map<CourseViewModel>(course);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var course = _mapper.Map<Course>(model);
                bool res = await courseRepository.UpdateAsync(course);
                actionResult = res ? RedirectToAction("Index") : RedirectToAction("Edit", model.Id);
            }
            else
            {
                actionResult = RedirectToAction("Edit", model.Id);
            }
            return actionResult;
        }

        public async Task<IActionResult> Delete(int Id)
        {

            IActionResult actionResult;
            Course? course = await courseRepository.GetByIdAsync(Id);
            if (course != null)
            {
                var model = _mapper.Map<CourseViewModel>(course);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }   
        [HttpPost]
        public async Task<IActionResult> Delete(CourseViewModel model)
        {
            IActionResult actionResult;
            Course? course = await courseRepository.GetByIdAsync(model.Id);
            if (course != null)
            {
                await courseRepository.DeleteAsync(course.Id);
                actionResult = RedirectToAction("Index");
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
    }


    
}
