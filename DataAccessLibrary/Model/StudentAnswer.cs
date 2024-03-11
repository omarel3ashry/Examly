namespace DataAccessLibrary.Model
{
    public class StudentAnswer
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int ChoiceId { get; set; }
        public Choice Choice { get; set; }
    }
}
