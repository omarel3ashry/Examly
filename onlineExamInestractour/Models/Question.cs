namespace onlineExamInestractour.Models
{
    public enum QuestionType
    {
        TrueFalse,
        MultipleChoice
    }

    public enum Difficulty {
        Easy,
        Medium,
        Hard
    }

    public enum Grade
    {
        A,
        B,
        C,
        D
    }
    public class Question
    {
        public int ? QuestionId { get; set; }
        public string QuestionText { get; set; }
        
        public QuestionType Type { get; set; }
        public Difficulty Difficulty { get; set; }
        public Grade Grade { get; set; }
        public ICollection<Choice> ?Choices { get; set; }
        public ICollection<ExamQuestions> ExamsQuestions { get; } = new HashSet<ExamQuestions>();
        public int CourseId { get; set; }
        public Course ?Course { get; set; }
    }
}