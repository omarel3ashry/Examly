using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppProject.Models;


namespace WebAppProject.Controllers
{
    public class BranchController : Controller
    {
        IEnumerable<Branch> branshes = Branch.BranchDemo;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int Id)
        {
            Branch? model = branshes.SingleOrDefault(a => a.Id == Id);
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
            Debug.WriteLine("From create " + Branch.Id);
            Debug.WriteLine(Branch.Name);
            int res = 1;
            IActionResult actionResult = res > 0 ? RedirectToAction("Index", "Dashboard") : RedirectToAction("Create");
            return actionResult;
        }

        public IActionResult Edit(int Id)
        {
            Branch? model = branshes.SingleOrDefault(a => a.Id == Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Edit(Branch Branch,int Id)
        {
            Branch old =  branshes.FirstOrDefault(a => a.Id == Id)!;
            old.Name = Branch.Name;
            Debug.WriteLine("From Edit "+Id);
            Debug.WriteLine(Branch.Name);
            int res = 1;
            IActionResult actionResult = res > 0 ? RedirectToAction("Index","Dashboard") : RedirectToAction("Edit");
            return actionResult;
        }

        public IActionResult Delete(int Id)
        {
            Debug.WriteLine("From delete View " + Id);
            Branch? model = branshes.SingleOrDefault(a => a.Id == Id);
            IActionResult actionResult = model != null ? View(model) : BadRequest();
            return actionResult;
        }
        [HttpPost]
        public IActionResult Delete(Branch Branch, int Id)
        {
            Debug.WriteLine("From delete " + Id);
            return RedirectToAction("Index", "Dashboard");
        }


    }
}
