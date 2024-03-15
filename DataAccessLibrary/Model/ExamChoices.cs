namespace DataAccessLibrary.Model
{
    public class ExamChoices
    {
        public Question Question { get; set; }
        public List<Choice> CorrectChoices { get; set; }
        public List<Choice> StudentChoices { get; set; }
        public bool IsCorrect { get; set; }
    }
}
