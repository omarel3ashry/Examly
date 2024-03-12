using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository=studentRepository;
        }
        public IActionResult Index()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var model = studentRepository.GetById(id);
            return View(model);
        }
    }
}
