using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAppProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class StudentController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IExamTakenRepository examTaken;

        public StudentController(IInstructorRepository instructorRepository,
            IDepartmentRepository departmentRepository,
            IStudentRepository studentRepository,
            IBranchRepository branchRepository,
            IExamTakenRepository examTaken)
        {
            this.instructorRepository = instructorRepository;
            this.departmentRepository = departmentRepository;
            this.studentRepository = studentRepository;
            this.branchRepository = branchRepository;
            this.examTaken = examTaken;
        }
        public IActionResult Index(int deptId)
        {
            var model = studentRepository.SelectAll(st => !st.IsDeleted && st.DepartmentId == deptId);
            return View(model);
        }
        public IActionResult Grades(int stId)
        {
            var deptId = studentRepository.GetById(stId)?.DepartmentId;
            var model = examTaken.GetAllByStudentIdWithIncludes(stId);
            ViewBag.DeptId = deptId ?? 0;
            return View(model);
        }
        public IActionResult Answers(int stId, int examId)
        {
            var model = studentRepository.GetStudentAnswers(stId, examId);
            ViewBag.StId = stId;
            return View(model);
        }

        public IActionResult Delete(int stId)
        {
            var student = studentRepository.GetById(stId);
            studentRepository.Delete(stId);
            return student != null ? RedirectToAction("Index", new { deptId = student.DepartmentId }) : BadRequest();
        }
    }
}
