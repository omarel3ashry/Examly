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
    public class BranchController : Controller
    {
        private readonly IBranchRepository branchRepository;
        private readonly IMapper _mapper;

        public BranchController(IBranchRepository branchRepository,
                                IMapper mapper)
        {
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
            Branch? branch = await branchRepository.GetByIdAsync(Id);
            if (branch != null)
            {
                var model = _mapper.Map<BranchViewModel>(branch);
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
        public async Task<IActionResult> Create(BranchViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var branch = _mapper.Map<Branch>(model);
                int res = await branchRepository.AddAsync(branch);
                actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
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
            Branch? branch = await branchRepository.GetByIdAsync(Id);
            if (branch != null)
            {
                var model = _mapper.Map<BranchViewModel>(branch);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BranchViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var branch = _mapper.Map<Branch>(model);
                bool res = await branchRepository.UpdateAsync(branch);
                actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit", model.Id);
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
            Branch? branch = await branchRepository.GetByIdAsync(Id);
            if (branch != null)
            {
                var model = _mapper.Map<BranchViewModel>(branch);
                actionResult = View(model);
            }
            else
            {
                actionResult = BadRequest();
            }
            return actionResult;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BranchViewModel model)
        {
            IActionResult actionResult;
            Branch? branch = await branchRepository.GetByIdAsync(model.Id);
            if (branch != null)
            {
                await branchRepository.DeleteAsync(branch.Id);
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
