using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;



namespace WebAppProject.Controllers
{
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
            
            int res = branchRepository.Add(Branch);
            IActionResult actionResult = res > 0 ? RedirectToAction("Index", "Manage") : RedirectToAction("Create");
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            Branch? model = branchRepository.GetById(Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Edit(Branch Branch,int Id)
        {
            Branch old =  branchRepository.GetById(Id)!;
            old.Name = Branch.Name;
       
            bool res = branchRepository.Update(old);
            IActionResult actionResult = res ? RedirectToAction("Index","Manage") : RedirectToAction("Edit");
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
