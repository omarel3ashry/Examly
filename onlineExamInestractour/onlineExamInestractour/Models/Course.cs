namespace onlineExamInestractour.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int DepartmentId { get; set; }
        public Department ?Department { get; set; }
        public int ?InstructorId { get; set; }
        public Instructor ?Instructor { get; set; }
        public ICollection<Question> ?Questions { get; set; }
        public ICollection<Student> ?Students { get; set; }
        public ICollection<Exam> ?Exams { get; set; }
    }
}