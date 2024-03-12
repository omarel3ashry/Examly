namespace DataAccessLibrary.Model
{
    public class ExamTaken
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int Grade { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Student Student { get; set; }
        public Exam Exam { get; set; }
    }
}
