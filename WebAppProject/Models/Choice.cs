using System.Security.Cryptography.X509Certificates;

namespace WebAppProject.Models
{
    public class Choice
    {
        public static List<Choice> ChoiceDemo = [
            new Choice {Id=1,Body="blue", IsCorrect = true,QuestionId = 1, Question = Question.QuestionDemo[0] },
            new Choice {Id=2,Body="green", IsCorrect = false,QuestionId = 1, Question = Question.QuestionDemo[0] },
            new Choice {Id=3,Body="yellow", IsCorrect = false,QuestionId = 1, Question = Question.QuestionDemo[0] },
            new Choice {Id=4,Body="black", IsCorrect = false,QuestionId = 1, Question = Question.QuestionDemo[0] },
            new Choice {Id=5,Body="blue", IsCorrect = false,QuestionId = 2, Question = Question.QuestionDemo[1] },
            new Choice {Id=6,Body="green", IsCorrect = true,QuestionId = 2, Question = Question.QuestionDemo[1] },
            new Choice {Id=7,Body="yellow", IsCorrect = false,QuestionId = 2, Question = Question.QuestionDemo[1] },
            new Choice {Id=8,Body="black", IsCorrect = false,QuestionId = 2, Question = Question.QuestionDemo[1] },
        ];
        public int Id { get; set; }
        public string Body { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
