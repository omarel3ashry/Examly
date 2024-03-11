using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using WebAppProject.Repository;

namespace WebAppProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ManageController : Controller
    {
        IInstructorRepository instructorRepository ;
       
        private readonly IDepartmentRepository departmentRepository;
        private readonly IStudentRepository studentRepository;

        public ManageController(IInstructorRepository instructorRepository ,IDepartmentRepository departmentRepository,IStudentRepository studentRepository)
        {
            this.instructorRepository = instructorRepository;
            this.departmentRepository = departmentRepository;
            this.studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetLists(int BranchId)
        {
            var DepartmentList = departmentsRepository .OrderBy(d => d.Name).ToList();
            var InstructorList = instructors.Where(i => i.BranchId == BranchId).OrderBy(i => i.Name).ToList();
            return Ok(new { DepartmentList, InstructorList });
        }
        public IActionResult DepartmentManager(int id)
        {
            var model=instructorRepository.SelectAll(e=>e.BranchId==id);
            return PartialView(model);
        }
        public IActionResult AssignManager(int deptId,int insId)
        {

            departmentRepository.SetManager(deptId, insId);
            return RedirectToAction("Index");
        }
        public IActionResult Students(int id)
        {
            var model = studentRepository.SelectAll(st=>st.DepartmentId==id);

            return PartialView(model);
        }
        public IActionResult StudentGrades(int stId)
        {
            var model= studentRepository.GetGrades(stId);
            return View(model);
        }
        public IActionResult StudentAnswers(int stId,int examId)
        {
            var model=studentRepository.GetAnswers(stId,examId);
            return View(model);
        }
        public IActionResult StudentDelete(int stId)
        {
            studentRepository.Delete(stId);
            return RedirectToAction("Index");
        }

    }
}
