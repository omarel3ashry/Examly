namespace DataAccessLibrary.Model
{
    public enum QType
    {
        Unspecified = 0,
        MCQ = 1,
        TrueFalse = 2
    }
    public enum QDifficulty { Unspecified = 0, Easy = 1, Medium = 2, Hard = 3 }
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QType Type { get; set; }
        public int Grade { get; set; }
        public QDifficulty Difficulty { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Choice> Choices { get; } = new List<Choice>();
        public ICollection<Exam> Exams { get; } = new List<Exam>();
    }
}
