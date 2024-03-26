using DataAccessLibrary.Model;

namespace WebAppProject.VMS
{
    public class DepartmentCourseViewModel
    {
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public int? InstructorId { get; set; }
        public string? InstructorName { get; set; }
    }
}
