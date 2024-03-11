namespace WebAppProject.Models
{
    public class Student
    {
        public static List<Student> StudentDemo= [
                new Student() { Id = 1, Name = "hassan", DeptId = 1, Department = Department.DepartmentDemo[0], UserId = 4, User = User.UserDemo[3] },
            new Student() { Id = 2, Name = "loay", DeptId = 1, Department = Department.DepartmentDemo[0] },
            new Student() { Id = 3, Name = "madgy", DeptId = 2, Department = Department.DepartmentDemo[1] },
            new Student() { Id = 4, Name = "omar", DeptId = 2, Department = Department.DepartmentDemo[1]}

            ];
        public int Id { get; set; }
        public string Name { get; set; }

        public int DeptId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Department Department { get; set; }
    }
}
