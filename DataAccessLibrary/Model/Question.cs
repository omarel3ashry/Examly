namespace DataAccessLibrary.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public int Grade { get; set; }
        public int Difficulty { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<Choice> Choices { get; } = new List<Choice>();
        public ICollection<Exam> Exams { get; } = new List<Exam>();
    }
}
