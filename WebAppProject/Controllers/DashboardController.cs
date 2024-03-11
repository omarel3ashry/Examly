using Microsoft.AspNetCore.Mvc;
using WebAppProject.FakeModels;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class DashboardController : Controller
    {
        IEnumerable<Branch> branshes = new List<Branch>()
        { new Branch {BranchId=1 , Name = "Alex" }, new Branch { BranchId = 2, Name = "Smart" },
         new Branch {BranchId=3 , Name = "New Capital" }, new Branch {BranchId=4 , Name = "Monofia" }};

        IEnumerable<Instructor> instructors = new List<Instructor>()
        {
            new Instructor {InstructorId=1, Name="Mohamed", BranchId = 1},
            new Instructor {InstructorId=2, Name="Ahmed", BranchId = 1},
            new Instructor {InstructorId=3, Name="Omar", BranchId = 2},
            new Instructor {InstructorId=4, Name="Karim", BranchId = 2},
            new Instructor {InstructorId=5, Name="Nourhan", BranchId = 3},
            new Instructor {InstructorId=6, Name="Hossam", BranchId = 3},
            new Instructor {InstructorId=7, Name="Aly", BranchId = 4},

        };

        IEnumerable<Department> departments = new List<Department>()
        {
            new Department {DepartmentId=1 ,Name ="CS", BranchId = 1},
            new Department {DepartmentId=2 ,Name ="SE", BranchId = 1},
            new Department {DepartmentId=3 ,Name ="CS", BranchId = 2},
            new Department {DepartmentId=4 ,Name ="SE", BranchId = 3},
            new Department {DepartmentId=5 ,Name ="IT", BranchId = 2},
            new Department {DepartmentId=6 ,Name ="AI", BranchId = 2},
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
