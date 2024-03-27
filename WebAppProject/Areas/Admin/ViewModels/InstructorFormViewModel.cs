using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.Areas.Admin.ViewModels
{
    public class InstructorFormViewModel
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "Name must be at least 3 characters.")]
        [RegularExpression(@"[a-zA-Z- ]+", ErrorMessage = "Name must contain only characters")]
        public string Name { get; set; }
        
        [RegularExpression(@"^[\w-]+(\.[\w-]+)*@([\w -]+\.)+[a-zA-Z]{2,7}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters.")]
        public string Password { get; set; }

        [MinLength(7, ErrorMessage = "Phone must be at least 7 Numbers.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Name must contain only Numbers")]
        public string Phone { get; set; }

        [MinLength(4, ErrorMessage = "Adderss must be at least 4 characters.")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "You must select gender.")]
        public string Gender { get; set; }

        [Range(22,80, ErrorMessage = "Age must be at between 22 and 80.")]
        public int Age { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must select Branch first.")]
        public int BranchId { get; set; }
        public int? UserId { get; set; }

        public List<BranchViewModel>? Branches { get; set; }
        public List<SelectListItem>? GenderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };


    }
}
