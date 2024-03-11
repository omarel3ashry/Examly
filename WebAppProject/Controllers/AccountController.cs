using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAppProject.Models;
using WebAppProject.Repository;
using WebAppProject.ViewModels;

namespace WebAppProject.Controllers
{
    public class AccountController : Controller
    {
        UserRepo _userRepo= new UserRepo();
        StudentRepo _studentRepo= new StudentRepo();
        DepartmentRepo _departmentRepo= new DepartmentRepo();
        BranchRepo _branchRepo= new BranchRepo();
        InstructorRepo _instructorRepo= new InstructorRepo();
        public IActionResult Register()
        {
            ViewBag.Branches = _branchRepo.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Register(Register registerInfo) 
        { 
            User user = new User { Email = registerInfo.Email, Password = registerInfo.Password};
            int userId= _userRepo.AddStudent(user);
            Student student= new Student { Name=registerInfo.Name,DeptId=int.Parse(registerInfo.DeptId),UserId=userId};
            _studentRepo.Add(student);
            return RedirectToAction("Login"); 
        }
        public IActionResult Departments(int id)
        {
            var model= _departmentRepo.GetAllByBranchId(id);
            return PartialView(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User LoginInfo)
        {
            User user = _userRepo.CheckUser(LoginInfo);
            if (user==null)
            {
                ModelState.AddModelError("", "Username OR Password Invalid");
                return View(LoginInfo);
            }
            string role = user.Role.Name;
            Claim roleClaim = new Claim(ClaimTypes.Role, role);
            ClaimsIdentity identity = new ClaimsIdentity("Cookies");
            identity.AddClaim(roleClaim);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            string controller;
            int id;
            if (role == "Student")
            {
                id = _studentRepo.GetByUserId(user.Id).Id;
                controller = "Student";
            }
            else if(role == "Admin")
            {
                id = 0;
                controller = "Manage";
            }
            else 
            {
                id= _instructorRepo.GetByUserId(user.Id).Id;
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
