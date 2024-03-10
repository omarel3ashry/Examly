namespace onlineExamInestractour.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}