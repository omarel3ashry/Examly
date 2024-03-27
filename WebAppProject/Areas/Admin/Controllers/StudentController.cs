using AutoMapper;
using DataAccessLibrary.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Areas.Admin.ViewModels;
using WebAppProject.ViewModels;

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
        private readonly IExamTakenRepository examTakenRepo;
        private readonly IMapper _mapper;

        public StudentController(IInstructorRepository instructorRepository,
            IDepartmentRepository departmentRepository,
            IStudentRepository studentRepository,
            IBranchRepository branchRepository,
            IExamTakenRepository examTakenRepo,
            IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.departmentRepository = departmentRepository;
            this.studentRepository = studentRepository;
            this.branchRepository = branchRepository;
            this.examTakenRepo = examTakenRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int deptId)
        {
            var students = await studentRepository.SelectAllAsync(st => !st.IsDeleted && st.DepartmentId == deptId);
            var model = _mapper.Map<IEnumerable<StudentViewModel>>(students);
            return View(model);
        }
        public async Task<IActionResult> Grades(int stId)
        {
            var department = await studentRepository.GetByIdAsync(stId);
            var deptId = department?.DepartmentId;
            var examsTaken = await examTakenRepo.GetAllByStudentIdWithIncludesAsync(stId);
            IEnumerable<AdminExamTakenViewModel> model =
                _mapper.Map<IEnumerable<AdminExamTakenViewModel>>(examsTaken);
            ViewBag.DeptId = deptId ?? 0;
            return View(model);
        }
        public async Task<IActionResult> Answers(int stId, int examId)
        {
            var studentAnswers = await studentRepository.GetStudentAnswersAsync(stId, examId);
            var model = _mapper.Map<IEnumerable<StudentAnswersViewModel>>(studentAnswers);
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
