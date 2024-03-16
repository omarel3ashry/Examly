using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ManageInstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IBranchRepository _branchRepo;
        private readonly IUserRepository _userRepo;

        public ManageInstructorController(IInstructorRepository instructorRepository,
                                    IBranchRepository branchRepository,
                                    IUserRepository userRepo)
        {
            _instructorRepo = instructorRepository;
            _branchRepo = branchRepository;
            _userRepo = userRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {
            Instructor? model = _instructorRepo.GetByIdWithIncludes(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        public IActionResult Create()
        {
            List<Branch> branches = _branchRepo.GetAll();
            var model = new InstructorWithBranchesViewModel() { Branches = branches };

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(InstructorWithBranchesViewModel model)
        {
            var user = new User() { Email = model.Email, Password = model.Password, RoleId = 3 };
            int userId = _userRepo.Add(user);
            if (userId != 0)
            {
                var instructorDto = model.ToInstructorDto();
                instructorDto.UserId = userId;
                int instId = _instructorRepo.Add(instructorDto);
                return instId > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            }
            return RedirectToAction("Create");
        }

        public IActionResult Edit(int Id)
        {
            IActionResult actionResult = BadRequest();
            List<Branch> branches = _branchRepo.GetAll();
            Instructor? instructor = _instructorRepo.GetByIdWithIncludes(Id);
            if (instructor != null)
            {
                var model = new InstructorWithBranchesViewModel(instructor, branches);
                actionResult = View(model);
            }
            return actionResult;
        }

        [HttpPost]
        public IActionResult Edit(Instructor Instructor, int Id)
        {
            Instructor.Id = Id;
            bool res = _instructorRepo.Update(Instructor);
            IActionResult actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit");
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {
            Instructor? model = _instructorRepo.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Delete(Instructor Instructor)
        {
            _instructorRepo.Delete(Instructor.Id);
            return RedirectToAction("Index", "Manage");
        }
    }

}
