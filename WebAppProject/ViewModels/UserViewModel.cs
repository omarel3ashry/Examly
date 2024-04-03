using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "*")]
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w -]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters")]
        public string Password { get; set; }
    }
}
