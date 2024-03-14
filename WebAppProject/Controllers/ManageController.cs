using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Drawing;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly IInstructorRepository instructorRepository;     
        private readonly IDepartmentRepository departmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IExamTakenRepository examTaken;
        

        public ManageController(IInstructorRepository instructorRepository ,
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
        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel() { Branches = branchRepository.GetAll() };
            return View(model);
        }
      
        public IActionResult GetLists(int BranchId)
        {   
            var DepartmentList = departmentRepository.SelectAll(dept=>dept.BranchId==BranchId).Select(e => new { e.Id, e.Name }).OrderBy(e=>e.Name);          
            var InstructorList = instructorRepository.SelectAll(e=>e.BranchId==BranchId).Select(e => new { e.Id, e.Name }).OrderBy(e => e.Name);                 
            return Ok(new { DepartmentList, InstructorList });
        }
        public IActionResult DepartmentManager(int branchId,int deptId)
        {
            ViewBag.DepartmentId = deptId;
            var model =instructorRepository.SelectAll(e => e.BranchId == branchId);
            return PartialView(model);
        }
        public IActionResult AssignManager(int deptId, int insId)
        {

            departmentRepository.SetManager(deptId, insId);
            return RedirectToAction("Index");
        }
        public IActionResult Students(int deptId)
        {
            var model =studentRepository.SelectAll(st => st.DepartmentId == deptId);
            return View(model);
            
        }
        public IActionResult StudentGrades(int stId)
        {
            var model = examTaken.GetAllByStudentIdWithIncludes(stId);
            return View(model);
        }
        public IActionResult StudentAnswers(int stId, int examId)
        {
            var model=studentRepository.GetStudentAnswers(stId,examId);
            return View(model);
        }
        public IActionResult StudentDelete(int stId)
        {
            studentRepository.Delete(stId);
            return RedirectToAction("Index");
        }

    }
}
