namespace DataAccessLibrary.Model
{
    public class Student
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
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public ICollection<Exam> Exams { get; } = new List<Exam>();
        public ICollection<StudentAnswer> StudentAnswers { get; } = new HashSet<StudentAnswer>();
    }
}
