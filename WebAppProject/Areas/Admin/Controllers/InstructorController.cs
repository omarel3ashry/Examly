using AutoMapper;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Areas.Admin.ViewModels;
using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IBranchRepository _branchRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository, IBranchRepository branchRepository,
                                    IUserRepository userRepo, IMapper mapper)
        {
            _instructorRepo = instructorRepository;
            _branchRepo = branchRepository;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int Id)
        {
            IActionResult actionResult;
            Instructor? instructor = await _instructorRepo.GetByIdWithIncludesAsync(Id);
            if (instructor != null)
            {
                var model = _mapper.Map<InstructorViewModel>(instructor);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
        public async Task<IActionResult> Create()
        {
            List<Branch> branches = await _branchRepo.GetAllAsync();
            var branchesDtos = _mapper.Map<List<BranchViewModel>>(branches);
            var model = new InstructorFormViewModel() { Branches = branchesDtos };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(InstructorFormViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var user = new User() { Email = model.Email, Password = model.Password, RoleId = 3 };
                int userId = await _userRepo.AddAsync(user);
                if (userId != 0)
                {
                    var instructorDto = _mapper.Map<Instructor>(model);
                    instructorDto.UserId = userId;
                    int res = await _instructorRepo.AddAsync(instructorDto);
                    actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
                }
                else
                {
                    actionResult = RedirectToAction("Create");
                }
            }
            else
            {
                var branches = await _branchRepo.GetAllAsync();
                model.Branches = _mapper.Map<List<BranchViewModel>>(branches);
                actionResult = View(model);
            }
            return actionResult;
        }

        public async Task<IActionResult> Edit(int Id)
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
        public async Task<IActionResult> Edit(Instructor instructor, int Id)
        {
            instructor.Id = Id;
            bool res = _instructorRepo.Update(instructor);
            IActionResult actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit");
            return actionResult;
        }

        public async Task<IActionResult> Delete(int Id)
        {
            Instructor? model = _instructorRepo.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Instructor Instructor)
        {
            _instructorRepo.Delete(Instructor.Id);
            return RedirectToAction("Index", "Manage");
        }
    }

}
