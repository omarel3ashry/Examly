using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        public IActionResult Details(int Id)
        {
            Branch? model = branchRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BranchViewModel model)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                var branchDto = _mapper.Map<Branch>(model);
                int res = branchRepository.AddAsync(branchDto).Result;
                actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            }
            else
            {
                actionResult = View(model);
            }
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            Branch? model = branchRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }

        [HttpPost]
        public IActionResult Edit(Branch Branch)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                bool res = branchRepository.Update(Branch);
                actionResult = res ? RedirectToAction("Index", "Manage") : RedirectToAction("Edit", Branch.Id);
            }
            else
            {
                actionResult = RedirectToAction("Edit", Branch.Id);
            }
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {
            Branch? model = branchRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }

        [HttpPost]
        public IActionResult Delete(Branch Branch)
        {
            Debug.WriteLine(Branch.Id);
            branchRepository.Delete(Branch.Id);
            return RedirectToAction("Index", "Manage");
        }


    }
}
