using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebAppProject.Models;

namespace WebAppProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
                string controller = role == "Admin" ? "Manage" : role == "Student" ? "Student" : "Instructor";
                string area = role == "Admin" ? "Admin" : role == "Instructor" || role == "Manager" ? "Staff" : "";
                return RedirectToAction("Index", controller, new { area });
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.StatusCode = statusCode;
            if (statusCode == 404)
                ViewBag.StatusCodeMsg = "The page you’re looking for was not found.";
            else if (statusCode == 401)
                ViewBag.StatusCodeMsg = "You don’t have access to this page.";
            else
                ViewBag.StatusCodeMsg = "";
            return View("HttpError");
        }
    }
}
