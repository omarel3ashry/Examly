using DataAccessLibrary.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppProject.ViewModels
{
    public class InstructorWithBranchesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int? BranchId { get; set; }
        public int? UserId { get; set; }
        public List<Branch>? Branches { get; set; }
        public List<SelectListItem> GenderOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "m", Text = "Male" },
            new SelectListItem { Value = "f", Text = "Female" }
        };

        public InstructorWithBranchesViewModel() { }

        public InstructorWithBranchesViewModel(Instructor instructorDto, List<Branch> branches)
        {
            Id = instructorDto.Id;
            Name = instructorDto.Name;
            Phone = instructorDto.Phone;
            Address = instructorDto.Address;
            Gender = instructorDto.Gender;
            Age = instructorDto.Age;
            BranchId = instructorDto.BranchId;
            UserId = instructorDto.UserId;
            Branches = branches;
        }

        public Instructor ToInstructorDto()
        {
            return new Instructor()
            {
                Id = Id,
                Name = Name,
                Phone = Phone,
                Address = Address,
                Gender = Gender,
                Age = Age,
                BranchId = BranchId,
                UserId = UserId
            };
        }


    }
}
