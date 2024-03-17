namespace DataAccessLibrary.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int? ManagerId { get; set; }
        public Instructor? Manager { get; set; }
        public DateTime? HireDate { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Student> Students { get; } = new HashSet<Student>();
        public ICollection<DepartmentCourse> DepartmentCourses { get; } = new HashSet<DepartmentCourse>();
        public ICollection<Exam> Exams { get; } = new List<Exam>();
    }
}
