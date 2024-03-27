using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Areas.Admin.ViewModels;

namespace WebAppProject.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorRepository instructorRepository, IBranchRepository branchRepository,
                                    IUserRepository userRepository, IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.branchRepository = branchRepository;
            this.userRepository = userRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            IActionResult actionResult;
            Instructor? instructor = await instructorRepository.GetByIdWithIncludesAsync(Id);
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
            List<Branch> branches = await branchRepository.GetAllAsync();
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
                int userId = await userRepository.AddAsync(user);
                if (userId != 0)
                {
                    var instructorDto = _mapper.Map<Instructor>(model);
                    instructorDto.UserId = userId;
                    int res = await instructorRepository.AddAsync(instructorDto);
                    actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
                }
                else
                {
                    actionResult = RedirectToAction("Create");
                }
            }
            else
            {
                var branches = await branchRepository.GetAllAsync();
                model.Branches = _mapper.Map<List<BranchViewModel>>(branches);
                actionResult = View(model);
            }
            return actionResult;
        }

        public async Task<IActionResult> Edit(int Id)
        {
            IActionResult actionResult = BadRequest();

            Instructor? instructor = await instructorRepository.GetByIdWithIncludesAsync(Id);
            if (instructor != null)
            {
                List<Branch> branches = await branchRepository.GetAllAsync();
                var branchesDtos = _mapper.Map<List<BranchViewModel>>(branches);
                var model = _mapper.Map<InstructorFormViewModel>(instructor);
                model.Branches = branchesDtos;
                actionResult = View(model);
            }
            return actionResult;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorFormViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var instructor = _mapper.Map<Instructor>(model);
                bool res = await instructorRepository.UpdateAsync(instructor);
                actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit", model.Id);
            }
            else
            {
                var branches = await branchRepository.GetAllAsync();
                model.Branches = _mapper.Map<List<BranchViewModel>>(branches);
                actionResult = View(model);
            }
            return actionResult;
        }

        public async Task<IActionResult> Delete(int Id)
        {
            IActionResult actionResult = BadRequest();

            Instructor? instructor = await instructorRepository.GetByIdAsync(Id);
            if (instructor != null)
            {
                var model = _mapper.Map<InstructorViewModel>(instructor);
                actionResult = View(model);
            }
            return actionResult;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InstructorViewModel model)
        {
            IActionResult actionResult;
            Instructor? instructor = await instructorRepository.GetByIdAsync(model.Id);
            if (instructor != null)
            {
                await instructorRepository.DeleteAsync(instructor.Id);
                actionResult = RedirectToAction("Index", "Manage");
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
    }
}
