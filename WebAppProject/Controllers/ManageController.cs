using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IDepartmentRepository _departmentRepo;
        private readonly IStudentRepository _studentRepo;

        public ManageController(IInstructorRepository instructorRepository, IDepartmentRepository departmentRepository, IStudentRepository studentRepository)
        {
            this._instructorRepo = instructorRepository;
            this._departmentRepo = departmentRepository;
            this._studentRepo = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
       /* public IActionResult GetLists(int BranchId)
        {
            var DepartmentList = departmentsRepository.OrderBy(d => d.Name).ToList();
            var InstructorList = instructors.Where(i => i.BranchId == BranchId).OrderBy(i => i.Name).ToList();
            return Ok(new { DepartmentList, InstructorList });
        }*/
        public IActionResult DepartmentManager(int id)
        {
            var model = _instructorRepo.SelectAll(e => e.BranchId == id);
            return PartialView(model);
        }
        public IActionResult AssignManager(int deptId, int insId)
        {

            _departmentRepo.SetManager(deptId, insId);
            return RedirectToAction("Index");
        }
        public IActionResult Students(int id)
        {
            var model = _studentRepo.SelectAll(st => st.DepartmentId == id);

            return PartialView(model);
        }
  /*      public IActionResult StudentGrades(int stId)
        {
            var model = _studentRepo.GetGrades(stId);
            return View(model);
        }
        public IActionResult StudentAnswers(int stId, int examId)
        {
            var model = _studentRepo.GetAnswers(stId, examId);
            return View(model);
        }*/
        public IActionResult StudentDelete(int stId)
        {
            _studentRepo.Delete(stId);
            return RedirectToAction("Index");
        }

    }
}
