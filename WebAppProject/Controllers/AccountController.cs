using DataAccessLibrary.Model;
using DataAccessLibrary.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

using WebAppProject.Repository;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly IUserRepository userRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IInstructorRepository instructorRepository;

        public AccountController(IUserRepository userRepository,
            IStudentRepository studentRepository,
            IDepartmentRepository departmentRepository,
            IBranchRepository branchRepository,
            IInstructorRepository instructorRepository)
        {
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.branchRepository = branchRepository;
            this.instructorRepository = instructorRepository;
        }
        public IActionResult Register()
        {
            ViewBag.Branches = branchRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register registerInfo) 
        { 
            User user = new User { Email = registerInfo.Email, Password = registerInfo.Password,RoleId=4};
            int userId= userRepository.Add(user);
            Student student= new Student { Name=registerInfo.Name,DepartmentId=int.Parse(registerInfo.DeptId),UserId=userId};
            studentRepository.Add(student);
            return RedirectToAction("Login"); 
        }
        public IActionResult Departments(int id)
        {
            var model= departmentRepository.SelectAll(dept=>dept.BranchId==id);
            return PartialView(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User LoginInfo)
        {
            User user = userRepository.CheckUser(LoginInfo.Email,LoginInfo.Password);
            if (user==null)
            {
                ModelState.AddModelError("", "Username OR Password Invalid");
                return View(LoginInfo);
            }
            string role = user.Role.Title;
            Claim roleClaim = new Claim(ClaimTypes.Role, role);
            ClaimsIdentity identity = new ClaimsIdentity("Cookies");
            identity.AddClaim(roleClaim);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            string controller;
            int id;
            if (role == "Student")
            {
                id = studentRepository.GetByUserId(user.Id).Id;
                controller = "Student";
            }
            else if(role == "Admin")
            {
                id = 0;
                controller = "Manage";
            }
            else 
            {
                id= instructorRepository.GetByUserId(user.Id).Id;
                controller = "Instructor";
            }
            Claim idClaim = new Claim("id", id.ToString());
            identity.AddClaim(idClaim);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", controller);


        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
