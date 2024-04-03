using AutoMapper;
using DataAccessLibrary.Interfaces;
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

        public async Task<IActionResult> Index()
        {
            var branches = await branchRepository.GetAllAsync();
            var branchDtos = _mapper.Map<IEnumerable<BranchViewModel>>(branches);

            DashboardViewModel model = new DashboardViewModel() { Branches = branchDtos };
            return View(model);
        }

        public async Task<IActionResult> GetLists(int BranchId)
        {
            var departmentQuery = await departmentRepository.SelectAllAsync(dept => dept.BranchId == BranchId);
            var departmentList = departmentQuery.Select(e => new { e.Id, e.Name }).OrderBy(dept => dept.Name);
            var instructorQuery = await instructorRepository.SelectAllAsync(ins => ins.BranchId == BranchId);
            var instructorList = instructorQuery.Select(e => new { e.Id, e.Name }).OrderBy(ins => ins.Name);
            return Ok(new { departmentList, instructorList });
        }

        public async Task<IActionResult> DepartmentManager(int branchId, int deptId)
        {
            ViewBag.DepartmentId = deptId;
            var instructors = await instructorRepository.SelectAllAsync(e => e.BranchId == branchId);
            var model = _mapper.Map<IEnumerable<InstructorViewModel>>(instructors);
            return PartialView(model);
        }

        public async Task<IActionResult> AssignManager(int deptId, int insId)
        {

            await departmentRepository.SetManagerAsync(deptId, insId);
            return RedirectToAction("Index");
        }
    }
}
