using System.ComponentModel.DataAnnotations;

namespace WebAppProject.VMS
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "*")]
        //[RegularExpression("[a-zA-Z0-9_]+@[a-zA-Z]+.[a-zA-Z]{2,4}", ErrorMessage = "Invalid mail")] 
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        [MinLength(8, ErrorMessage = "Password should be atleast 8 characters")]
        public string Password { get; set; }
    }
}
