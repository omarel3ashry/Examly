namespace onlineExamInestractour.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public string Body { get; set; }
        public bool IsCorrect { get; set; }

        
        public int QuestionId { get; set; }
        
        public Question Question { get; set; }
        public override string ToString()
        {
            return $"{Body} - {(IsCorrect ? "Correct" : "Incorrect")}";
        }
    }
}