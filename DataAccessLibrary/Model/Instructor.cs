namespace DataAccessLibrary.Model
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int? UserId { get; set; }
        public User? User { get; set; }
        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }
        public Department? ManagedDepartment { get; set; }
        public ICollection<Question> Questions { get; } = new List<Question>();
        public ICollection<DepartmentCourse> DepartmentCourses { get; } = new List<DepartmentCourse>();
    }
}
