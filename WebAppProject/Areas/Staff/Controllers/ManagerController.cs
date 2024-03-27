using DataAccessLibrary.Interfaces;
using AutoMapper;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Areas.Staff.ViewModels;

namespace WebAppProject.Areas.Staff.Controllers
{
    [Authorize(Roles = "Manager")]
    [Area(areaName: "Staff")]
    public class ManagerController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IMapper _mapper;
        private readonly int _managerId;


        public ManagerController(IInstructorRepository instructorRepo,
                                     IDepartmentRepository deptRepo,
                                     ICourseRepository courseRepo,
                                     IHttpContextAccessor accessor,
                                     IMapper mapper)

        {
            _instructorRepo = instructorRepo;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
            _mapper = mapper;
            _managerId = int.Parse(accessor.HttpContext!.User.Claims.FirstOrDefault(c => c.Type == "id")!.Value);

        }

        public async Task<IActionResult> Index()
        {

            var department = await _deptRepo.GetByManagerIdCoursesIncludedAsync(_managerId);
            if (department == null)
            {
                return NotFound();
            }
            var departmentviewmodel = _mapper.Map<StaffDepartmentViewModel>(department);

            return View(departmentviewmodel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _deptRepo.SelectAsync(e => e.ManagerId == _managerId);

            if (department == null)
            {
                return NotFound();
            }

            var deptId = department.Id;
            var departmentCourse = await _deptRepo.GetDeptCourseWithIncludesAsync(id, deptId);

            if (departmentCourse == null)
            {
                return NotFound();
            }

            var courseinfoviewmodel = _mapper.Map<CourseInfoViewModel>(departmentCourse);
            return View(courseinfoviewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> AssignInstructor(int id)
        {
            var department = await _deptRepo.SelectAsync(e => e.ManagerId == _managerId);

            if (department == null)
            {
                return NotFound();
            }

            var deptId = department.Id;
            var departmentCourse = await _deptRepo.GetDeptCourseWithIncludesAsync(id, deptId);

            if (departmentCourse == null)
            {
                return NotFound();
            }

            var branchId = departmentCourse.Department.BranchId;
            var availableInstructors = await _instructorRepo.SelectAllAsync(e => e.BranchId == branchId);
            var selectList = new SelectList(availableInstructors, "Id", "Name", departmentCourse.InstructorId);

            ViewBag.CourseId = id;
            ViewBag.Instructors = selectList;

            return PartialView("AssignInstructorPartial");
        }

        [HttpPost]
        public async Task<IActionResult> AssignInstructor(int crsId, int instId)
        {
            var department = await _deptRepo.SelectAsync(e => e.ManagerId == _managerId);

            if (department == null)
            {
                return NotFound();
            }

            var deptId = department.Id;
            var course = await _courseRepo.GetByIdAsync(crsId);
            var instructor = await _instructorRepo.GetByIdAsync(instId);
            if (course == null || instructor == null)
            {
                return NotFound();
            }

            await _deptRepo.UpdateDeptCourseInstructorAsync(deptId, crsId, instId);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            var department = await _deptRepo.SelectAsync(e => e.ManagerId == _managerId);

            if (department != null)
            {
                var deptId = department.Id;
                await _deptRepo.DeleteDeptCourseAsync(deptId, id);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddCourses(int id)
        {
            var department = await _deptRepo.SelectAsync(e => e.ManagerId == _managerId);

            if (department == null)
            {
                return NotFound();
            }

            var deptId = department.Id;
            var coursesNotInDept = await _courseRepo.GetCoursesNotInDepartmentAsync(deptId);
            var addCoursesList = _mapper.Map<List<AddCourseViewModel>>(coursesNotInDept);

            ViewBag.DepartmentId = id;
            return View(addCoursesList);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourses(int id, List<AddCourseViewModel> coursesToAdd)
        {
            ViewBag.DepartmentId = id;
            if (ModelState.IsValid)
            {
                var deptCourses = new List<DepartmentCourse>();

                foreach (var course in coursesToAdd)
                {
                    if (course.WantToAdd)
                        deptCourses.Add(new DepartmentCourse() { DepartmentId = id, CourseId = course.Id });
                }

                int result = await _deptRepo.AddDepartmentCoursesAsync(deptCourses);
                return result == deptCourses.Count() ? RedirectToAction("Index") : View(coursesToAdd);
            }
            return View(coursesToAdd);
        }
    }
}
