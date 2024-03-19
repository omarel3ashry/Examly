namespace DataAccessLibrary.Model
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ExamDate { get; set; }
        public int DurationInMinutes { get; set; }
        public int? TotalGrade { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; } = new HashSet<Question>();
        public ICollection<Student> Students { get; } = new List<Student>();
        public ICollection<StudentAnswer> StudentAnswers { get; } = new HashSet<StudentAnswer>();
    }
}
