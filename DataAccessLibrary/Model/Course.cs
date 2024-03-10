namespace DataAccessLibrary.Model
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Topic> Topics{ get; } = new HashSet<Topic>();
        public ICollection<Question> Questions{ get; } = new HashSet<Question>();
        public ICollection<Exam> Exams{ get; } = new HashSet<Exam>();
        public ICollection<DepartmentCourse> DepartmentCourses { get; } = new HashSet<DepartmentCourse>();
    }
}
