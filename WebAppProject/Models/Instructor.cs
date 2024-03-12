using System.Collections.Generic;

namespace WebAppProject.Models
{
    public class Instructor
    {
        public static List<Instructor> InstructorDemo = [

            new Instructor() { Id = 65, Name = "Ahmed Karim",BranchId=1,Branch=Branch.BranchDemo[0], UserId = 2, User = User.UserDemo[1] },
            new Instructor() { Id = 52, Name = "Aly Mohamed",BranchId=1,Branch=Branch.BranchDemo[0], UserId = 3, User = User.UserDemo[2] },
            new Instructor() { Id = 26, Name = "Mohamed Samir", BranchId = 2, Branch = Branch.BranchDemo[1] },
            new Instructor() { Id = 82, Name = "Omar fares", BranchId = 2, Branch = Branch.BranchDemo[1] }


        ];
        public int Id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        Branch Branch { get; set; }
        public int BranchId { get; set; }
        public User User { get; set; }

    }
}
