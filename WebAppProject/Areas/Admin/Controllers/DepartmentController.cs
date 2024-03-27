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
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository departmentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IBranchRepository branchRepository, IMapper mapper)
        {
            this.departmentRepository = departmentRepository;
            this.branchRepository = branchRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int Id)
        {
            IActionResult actionResult;
            Department? department = await departmentRepository.GetByIdWithIncludesAsync(Id);
            if (department != null)
            {
                var model = _mapper.Map<AdminDepartmentViewModel>(department);
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
            var branches = await branchRepository.GetAllAsync();
            var branchesDtos = _mapper.Map<List<BranchViewModel>>(branches);
            var model = new DepartmentFormViewModel() { Branches = branchesDtos };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentFormViewModel model)
        {

            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                Department department = _mapper.Map<Department>(model);
                int res = await departmentRepository.AddAsync(department);
                actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
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
            IActionResult actionResult;
            Department? department = departmentRepository.GetByIdWithIncludes(Id);
            if (department != null)
            {
                var model = _mapper.Map<DepartmentFormViewModel>(department);
                var branches = await branchRepository.GetAllAsync();
                model.Branches = _mapper.Map<List<BranchViewModel>>(branches);

                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentFormViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var department = _mapper.Map<Department>(model);
                bool res = await departmentRepository.UpdateAsync(department);
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
            IActionResult actionResult;
            Department? department = await departmentRepository.GetByIdWithIncludesAsync(Id);
            if (department != null)
            {
                var model = _mapper.Map<AdminDepartmentViewModel>(department);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AdminDepartmentViewModel model)
        {
            IActionResult actionResult;
            Department? department = await departmentRepository.GetByIdWithIncludesAsync(model.Id);
            if (department != null)
            {
                await departmentRepository.DeleteAsync(department.Id);
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
