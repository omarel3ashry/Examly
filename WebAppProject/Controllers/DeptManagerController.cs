using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class DeptManagerController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseRepository _courseRepo;

        public DeptManagerController(IInstructorRepository instructorRepo,
                                     IDepartmentRepository deptRepo,
                                     ICourseRepository courseRepo)
        {
            _instructorRepo = instructorRepo;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
        }

        public IActionResult Index()
        {
            // TODO: get managed dept id , assuming this user manages deptNo 1
            var department = _deptRepo.GetByIdCoursesIncluded(1);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public IActionResult Details(int id)
        {
            // TODO: get managed dept id , assuming this user manages deptNo 1
            var departmentCourse = _deptRepo.GetDeptCourseWithIncludes(id, 1);
            if (departmentCourse == null)
            {
                return NotFound();
            }
            return View(departmentCourse);
        }

        [HttpGet]
        public IActionResult AssignInstructor(int id)
        {
            // TODO: get managed dept id , assuming this user manages deptNo 1
            var departmentCourse = _deptRepo.GetDeptCourseWithIncludes(id, 1);
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
            // TODO: get managed dept id , assuming this user manages deptNo 1
            var course = _courseRepo.GetById(crsId);
            var instructor = _instructorRepo.GetById(instId);
            if (course == null || instructor == null)
            {
                return NotFound();
            }

            _deptRepo.UpdateDeptCourseInstructor(1, crsId, instId);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCourse(int id)
        {
            // TODO: get managed dept id , assuming this user manages deptNo 1
            _deptRepo.DeleteDeptCourse(1, id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddCourses(int id)
        {
            // TODO: get managed dept id , assuming this user manages deptNo 1
            var coursesNotInDept = _courseRepo.GetCoursesNotInDepartment(id);
            var addCoursesList = new List<AddCourseViewModel>();

            foreach (var course in coursesNotInDept)
            {
                addCoursesList.Add(new AddCourseViewModel() { Id = course.Id, Name = course.Name });
            }

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
