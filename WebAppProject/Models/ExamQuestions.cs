namespace WebAppProject.Models
{
    public class ExamQuestions
    {
        public string Department { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public int TotalGrade { get; set; }
        public int DurationInHours { get; set; }
        public List<Question> Questions { get; set; }

    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
    }
}
