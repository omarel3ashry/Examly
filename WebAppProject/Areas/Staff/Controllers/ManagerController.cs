using AutoMapper;
using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.Areas.Staff.ViewModels;
using WebAppProject.ViewModels;

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

        public IActionResult Index()
        {        

            var department = _deptRepo.GetByManagerIdCoursesIncluded(_managerId);
            if (department == null)
            {
                return NotFound();
            }
            var departmentviewmodel = _mapper.Map<DepartmentViewModel>(department);

            return View(departmentviewmodel);
        }

        public IActionResult Details(int id)
        {
            var deptId= _deptRepo.Select(e=>e.ManagerId==_managerId)!.Id;
            var departmentCourse = _deptRepo.GetDeptCourseWithIncludes(id, deptId);
            if (departmentCourse == null)
            {
                return NotFound();
            }
            var courseinfoviewmodel = _mapper.Map<CourseInfoViewModel>(departmentCourse);
            return View(courseinfoviewmodel);
        }

        [HttpGet]
        public IActionResult AssignInstructor(int id)
        {
            var deptId = _deptRepo.Select(e => e.ManagerId == _managerId)!.Id;
            var departmentCourse = _deptRepo.GetDeptCourseWithIncludes(id, deptId);
            if (departmentCourse == null)
            {
                return NotFound();
            }
            var branchId = departmentCourse.Department.BranchId;
            var availableInstructors = _instructorRepo.SelectAll(e => e.BranchId == branchId);
            var selectList = new SelectList(availableInstructors, "Id", "Name", departmentCourse.InstructorId);

            ViewBag.CourseId = id;
            ViewBag.Instructors = selectList;

            return PartialView("AssignInstructorPartial");
        }

        [HttpPost]
        public IActionResult AssignInstructor(int crsId, int instId)
        {
            var deptId = _deptRepo.Select(e => e.ManagerId == _managerId)!.Id;           
            var course = _courseRepo.GetById(crsId);
            var instructor = _instructorRepo.GetById(instId);
            if (course == null || instructor == null)
            {
                return NotFound();
            }

            _deptRepo.UpdateDeptCourseInstructor(deptId, crsId, instId);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCourse(int id)
        {
            var deptId = _deptRepo.Select(e => e.ManagerId == _managerId)!.Id;
            _deptRepo.DeleteDeptCourse(deptId, id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddCourses(int id)
        {
            var deptId = _deptRepo.Select(e => e.ManagerId == _managerId)!.Id;
            var coursesNotInDept = _courseRepo.GetCoursesNotInDepartment(deptId);
            
            var addCoursesList = _mapper.Map<List<AddCourseViewModel>>(coursesNotInDept);


            ViewBag.DepartmentId = id;
            return View(addCoursesList);
        }

        [HttpPost]
        public IActionResult AddCourses(int id, List<AddCourseViewModel> coursesToAdd)
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

                int result = _deptRepo.AddDepartmentCourses(deptCourses);
                return result == deptCourses.Count() ? RedirectToAction("Index") : View(coursesToAdd);
            }
            return View(coursesToAdd);
        }
    }
}
