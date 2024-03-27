using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*")]
        [MinLength(3, ErrorMessage = "Name must be 3 characters or more.")]
        [RegularExpression(@"[a-zA-Z-]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression("[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}", ErrorMessage = "Invalid mail")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Email already exists")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "*")]
        [MinLength(4, ErrorMessage = "Address must be 4 characters or more.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^(01|\+201)[0125]\d{8}$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*")]
        public string Gender { get; set; }
        [Range(18, 30, ErrorMessage = "Age Must Be Between 18 and 30")]
        [Required(ErrorMessage = "*")]
        public int Age { get; set; }
        [Required(ErrorMessage = "*")]
        public int DeptId { get; set; }
        public List<SelectListItem> GenderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };
    }
}
