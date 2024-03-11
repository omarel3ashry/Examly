using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Models;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Instructor,Manager")]
    public class InstructorController : Controller
    {
        InstructorRepo _instructorRepo = new InstructorRepo();
        public IActionResult Index()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var model = _instructorRepo.GetById(id);
            return View(model);
        }
    }
}
