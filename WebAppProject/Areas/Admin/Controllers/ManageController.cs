using AutoMapper;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppProject.Areas.Admin.ViewModels;

namespace WebAppProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(areaName: "Admin")]
    public class ManageController : Controller
    {
        private readonly IInstructorRepository instructorRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IExamTakenRepository examTaken;
        private readonly IMapper _mapper;

        public ManageController(IInstructorRepository instructorRepository,
            IDepartmentRepository departmentRepository,
            IStudentRepository studentRepository,
            IBranchRepository branchRepository,
            IExamTakenRepository examTaken,
            IMapper mapper)
        {
            this.instructorRepository = instructorRepository;
            this.departmentRepository = departmentRepository;
            this.studentRepository = studentRepository;
            this.branchRepository = branchRepository;
            this.examTaken = examTaken;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var branches = branchRepository.GetAll();
            var branchDtos = _mapper.Map<IEnumerable<BranchViewModel>>(branches);

            DashboardViewModel model = new DashboardViewModel() { Branches = branchDtos };
            return View(model);
        }

        public IActionResult GetLists(int BranchId)
        {
            var DepartmentList = departmentRepository.SelectAll(dept => dept.BranchId == BranchId&& !dept.IsDeleted).Select(e => new { e.Id, e.Name }).OrderBy(dept => dept.Name);
            var InstructorList = instructorRepository.SelectAll(ins => ins.BranchId == BranchId&& !ins.IsDeleted).Select(e => new { e.Id, e.Name }).OrderBy(ins => ins.Name);
            return Ok(new { DepartmentList, InstructorList });
        }
        public IActionResult DepartmentManager(int branchId, int deptId)
        {
            ViewBag.DepartmentId = deptId;
            var instructors = instructorRepository.SelectAll(e => e.BranchId == branchId);
            var model = _mapper.Map<IEnumerable<InstructorViewModel>>(instructors);
            return PartialView(model);
        }
        public IActionResult AssignManager(int deptId, int insId)
        {

            departmentRepository.SetManager(deptId, insId);
            return RedirectToAction("Index");
        }
        

    }
}
