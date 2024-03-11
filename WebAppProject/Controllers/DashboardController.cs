using Microsoft.AspNetCore.Mvc;
using WebAppProject.FakeModels;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class DashboardController : Controller
    {
        IEnumerable<FakeBranch> branshes = new List<FakeBranch>()
        { new FakeBranch {BranchId=1 , Name = "Alex" }, new FakeBranch { BranchId = 2, Name = "Smart" },
         new FakeBranch {BranchId=3 , Name = "New Capital" }, new FakeBranch {BranchId=4 , Name = "Monofia" }};

        IEnumerable<FakeInstructor> instructors = new List<FakeInstructor>()
        {
            new FakeInstructor {InstructorId=1, Name="Mohamed", BranchId = 1},
            new FakeInstructor {InstructorId=2, Name="Ahmed", BranchId = 1},
            new FakeInstructor {InstructorId=3, Name="Omar", BranchId = 2},
            new FakeInstructor {InstructorId=4, Name="Karim", BranchId = 2},
            new FakeInstructor {InstructorId=5, Name="Nourhan", BranchId = 3},
            new FakeInstructor {InstructorId=6, Name="Hossam", BranchId = 3},
            new FakeInstructor {InstructorId=7, Name="Aly", BranchId = 4},

        };

        IEnumerable<FakeDepartment> departments = new List<FakeDepartment>()
        {
            new FakeDepartment {DepartmentId=1 ,Name ="CS", BranchId = 1},
            new FakeDepartment {DepartmentId=2 ,Name ="SE", BranchId = 1},
            new FakeDepartment {DepartmentId=3 ,Name ="CS", BranchId = 2},
            new FakeDepartment {DepartmentId=4 ,Name ="SE", BranchId = 3},
            new FakeDepartment {DepartmentId=5 ,Name ="IT", BranchId = 2},
            new FakeDepartment {DepartmentId=6 ,Name ="AI", BranchId = 2},
        };

        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel() { Branches = branshes };
            return View(model);
        }

        public IActionResult GetLists(int BranchId)
        {
            var DepartmentList = departments.Where(d => d.BranchId == BranchId).OrderBy(d => d.Name).ToList();
            var InstructorList = instructors.Where(i => i.BranchId == BranchId).OrderBy(i => i.Name).ToList();
            return Ok( new { DepartmentList, InstructorList });
        }
    }
}
