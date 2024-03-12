using WebAppProject.Models;

namespace WebAppProject.ViewModels
{
    public class QuestionInfo
    {
        public string CourseName { get; set; }
        public string QuestionText {  get; set; }
        public  QuestionType Type{ get; set; }
        public ICollection<Choice>? Choices { get; set; }

       
    }
}
