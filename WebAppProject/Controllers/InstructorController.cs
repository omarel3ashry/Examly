using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IBranchRepository branchRepository;
        
        public InstructorController(IInstructorRepository instructorRepository, IBranchRepository branchRepository)
        {
            this.instructorRepository = instructorRepository;
            this.branchRepository = branchRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {
            Instructor? model = instructorRepository.GetByIdWithIncludes(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        public IActionResult Create()
        {
            List<Branch> branches = branchRepository.GetAll();
            var model = new InstructorWithBranchesViewModel() { Branches = branches};
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Instructor Instructor)
        {

            int res = instructorRepository.Add(Instructor);
            IActionResult actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            IActionResult actionResult = BadRequest();
            List<Branch> branches = branchRepository.GetAll();
            Instructor? Instructor = instructorRepository.GetByIdWithIncludes(Id);
            if (Instructor != null)
            {
                var model = new InstructorWithBranchesViewModel() { Instructor = Instructor, Branches = branches };
                actionResult = View(model);
            }
            return actionResult;
        }
        [HttpPost]
        public IActionResult Edit(Instructor Instructor , int Id)
        {
            Instructor.Id = Id;
            bool res = instructorRepository.Update(Instructor);
            IActionResult actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit");
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {
            Instructor? model = instructorRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Delete(Instructor Instructor)
        {
            instructorRepository.Delete(Instructor.Id);
            return RedirectToAction("Index", "Manage");
        }
    }
}
