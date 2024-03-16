namespace DataAccessLibrary.Model
{
    public class DepartmentCourse
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
    }
}
