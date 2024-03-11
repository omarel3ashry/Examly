using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles ="Student")]
    public class StudentController : Controller
    {
        StudentRepo _studentRepo = new StudentRepo();
        public IActionResult Index()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id").Value);
            var model = _studentRepo.GetById(id);
            return View(model);
        }
    }
}
