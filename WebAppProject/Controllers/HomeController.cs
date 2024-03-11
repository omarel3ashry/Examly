using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBranchRepository _branchRepo;

        public HomeController(ILogger<HomeController> logger, IBranchRepository branchRepo)
        {
            _logger = logger;
            _branchRepo = branchRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            Branch branch = _branchRepo.Select(e => e.Id == 2);
            return Content($"id={branch.Id} name={branch.Name} location={branch.Location}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
