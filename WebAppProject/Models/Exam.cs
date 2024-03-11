namespace WebAppProject.Models
{
    public class Exam
    {
        static public List<Exam> ExamDemo = [
            
            new Exam() { Id = 1, Date = new DateTime(2024, 2, 27,9,0,0), CrsId = 1, course = Course.CourseDemo[0]},
            new Exam() { Id = 2, Date = new DateTime(2024, 2, 17, 9, 0, 0), CrsId = 2, course = Course.CourseDemo[1] },
            new Exam() { Id = 3, Date = new DateTime(2024, 1, 22, 9, 0, 0), CrsId = 3, course = Course.CourseDemo[2] },
            new Exam() { Id = 4, Date = new DateTime(2024, 2, 24, 9, 0, 0), CrsId = 4, course = Course.CourseDemo[3] },
            new Exam() { Id = 5, Date = new DateTime(2024, 1, 16, 9, 0, 0), CrsId = 5, course = Course.CourseDemo[4] },
        ];
        public int Id { get; set; }
        public int Duration { get; set; }

        public DateTime Date { get; set; }
        public int CrsId { get; set; }
        public Course course { get; set; }

    }
}
