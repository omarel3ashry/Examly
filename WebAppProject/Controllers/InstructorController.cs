using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppProject.Models;


namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Instructor,Manager")]
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        
        public InstructorController(IInstructorRepository instructorRepository)
        {
            this.instructorRepository = instructorRepository;
        }
        public IActionResult Index()
        {
            ViewBag.Role= User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var model = instructorRepository.GetByIdWithIncludes(id);
            return View(model);
        }
    }
}
