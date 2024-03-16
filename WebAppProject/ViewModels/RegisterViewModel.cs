using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppProject.ViewModels
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int DeptId { get; set; }
        public List<SelectListItem> GenderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };
    }
}
