namespace WebAppProject.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        

        public ICollection<Course> Courses { get; set; }
    }
}
