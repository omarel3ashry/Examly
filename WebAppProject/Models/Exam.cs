namespace WebAppProject.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
     
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<ExamQuestions> ExamsQuestions { get; set; }

        public Exam()
        {
            ExamsQuestions = new List<ExamQuestions>();
        }
    }
}