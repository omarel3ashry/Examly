using AutoMapper;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IMapper mapper;

        public AccountController(IUserRepository userRepository,
            IStudentRepository studentRepository,
            IDepartmentRepository departmentRepository,
            IBranchRepository branchRepository,
            IInstructorRepository instructorRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.branchRepository = branchRepository;
            this.instructorRepository = instructorRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Register()
        {
            var Branches = await branchRepository.GetAllAsync();
            ViewBag.Branches = Branches.ToDictionary(key => key.Id, value => value.Name);
            var model = new RegisterViewModel();
            return View(model);
        }
        public async Task<IActionResult> CheckEmail(string Email)
        {
            var st = await userRepository.GetByEmailAsync(Email);
            bool valid;
            valid = st == null ? true : false;
            return Json(valid);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerInfo)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = registerInfo.Email, Password = registerInfo.Password, RoleId = 4 };
                int userId = await userRepository.AddAsync(user);
                Student student = new Student
                {
                    Name = registerInfo.Name,
                    Age = registerInfo.Age,
                    Gender = registerInfo.Gender,
                    Phone = registerInfo.Phone,
                    Address = registerInfo.Address,
                    DepartmentId = registerInfo.DeptId,
                    UserId = userId
                };

                await studentRepository.AddAsync(student);
                return RedirectToAction("Login");
            }
            var Branches = await branchRepository.GetAllAsync();
            ViewBag.Branches = Branches.ToDictionary(key => key.Id, value => value.Name);
            return View(registerInfo);
        }

        public async Task<IActionResult> Departments(int id)
        {
            var depts = await departmentRepository.SelectAllAsync(dept => dept.BranchId == id);
            var model = mapper.Map<IList<DepartmentViewModel>>(depts);
            return PartialView(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel LoginInfo)
        {
            User? user = await userRepository.CheckUserAsync(LoginInfo.Email, LoginInfo.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Username OR Password Invalid");
                return View();
            }
            string role = user.Role!.Title;
            Claim roleClaim = new Claim(ClaimTypes.Role, role);
            ClaimsIdentity identity = new ClaimsIdentity("Cookies");
            identity.AddClaim(roleClaim);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            string area = "";
            string controller;
            int id;
            if (role == "Student")
            {
                var st = await studentRepository.GetByUserIdAsync(user.Id);
                id = st.Id;
                controller = "Student";
            }
            else if (role == "Admin")
            {
                id = 0;
                controller = "Manage";
                area = "Admin";
            }
            else
            {
                var ins = await instructorRepository.GetByUserIdAsync(user.Id);
                id = ins.Id;
                controller = "Instructor";
                area = "Staff";
            }
            Claim idClaim = new Claim("id", id.ToString());
            identity.AddClaim(idClaim);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index", controller, new { area });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
