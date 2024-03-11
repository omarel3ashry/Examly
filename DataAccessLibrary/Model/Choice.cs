namespace DataAccessLibrary.Model
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; } = new HashSet<StudentAnswer>();
    }
}
