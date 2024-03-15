using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppProject.ViewModels;



namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        
        private readonly IDepartmentRepository departmentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IInstructorRepository instructorRepository;

        public DepartmentController(IDepartmentRepository departmentRepository,IBranchRepository branchRepository)
        {
            this.departmentRepository = departmentRepository;
            this.branchRepository = branchRepository;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {
            Department? model = departmentRepository.GetByIdWithIncludes(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        public IActionResult Create()
        {
            List<Branch> branches = branchRepository.GetAll();
            var model = new DepartmentFormViewModel() { Branches = branches};
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Department Department)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                int res = departmentRepository.Add(Department);
                actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            }
            else
            {
                actionResult = RedirectToAction("Craete");
            }
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            IActionResult actionResult = BadRequest();
            List<Branch> branches = branchRepository.GetAll();
            Department? department = departmentRepository.GetByIdWithIncludes(Id);
            if (department != null)
            {
                var model = new DepartmentFormViewModel() { Department = department , Branches = branches };
                actionResult = View(model);
            }
            return actionResult;
        }
        [HttpPost]
        public IActionResult Edit(Department Department, int Id)
        {
            Department.Id = Id;
            bool res = departmentRepository.Update(Department);
            IActionResult actionResult = res ? RedirectToAction("Index","Manage") : RedirectToAction("Edit");
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {
            Department? model = departmentRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Delete(Department Department)
        {
            departmentRepository.Delete(Department.Id);
            return RedirectToAction("Index", "Manage");
        }


    }
}
