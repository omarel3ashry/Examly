using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var model = instructorRepository.GetById(id);
            return View(model);
        }
    }
}
