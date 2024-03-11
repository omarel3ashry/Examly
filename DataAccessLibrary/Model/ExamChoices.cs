namespace DataAccessLibrary.Model
{
    public class ExamChoices
    {
        /*    public ICollection<Question> ExamQuestions { get; set; }
            public ICollection<Question> StudentQuestions { get; set; }*/

        public Question Question { get; set; }
        public List<Choice> CorrectChoices { get; set; }
        public List<Choice> StudentChoices { get; set; }
    }
}
