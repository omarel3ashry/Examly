using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BranchController : Controller
    {
        
        private readonly IBranchRepository branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
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
        public IActionResult Create(Branch Branch)
        {
            IActionResult actionResult;
            if (ModelState.IsValid)
            {
                int res = branchRepository.Add(Branch);
                actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            }
            else
            {
                actionResult = View(Branch);
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
                actionResult = RedirectToAction("Edit",Branch.Id);
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
