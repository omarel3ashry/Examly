using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ManageController : Controller
    {
        InstructorRepo _instructorRepo = new InstructorRepo();
        DepartmentRepo _departmentRepo = new DepartmentRepo();
        StudentRepo _studentRepo = new StudentRepo();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DepartmentManager(int id)
        {
            var model=_instructorRepo.GetAllByBranchId(id);

            return PartialView(model);
        }
        public IActionResult AssignManager(int deptId,int insId)
        {
            var dept=_departmentRepo.GetById(deptId);
            dept.MgrId = insId;
            _departmentRepo.Update(dept);
            return RedirectToAction("Index");
        }
        public IActionResult Students(int id)
        {
            var model = _studentRepo.GetAllByDeptId(id);

            return PartialView(model);
        }
        public IActionResult StudentGrades(int stId)
        {
            var model= _studentRepo.GetGrades(stId);
            return View(model);
        }
        public IActionResult StudentAnswers(int stId,int examId)
        {
            var model=_studentRepo.GetAnswers(stId,examId);
            return View(model);
        }
        public IActionResult StudentDelete(int stId)
        {
            _studentRepo.Delete(stId);
            return RedirectToAction("Index");
        }

    }
}
