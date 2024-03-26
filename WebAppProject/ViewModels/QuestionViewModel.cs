using DataAccessLibrary.Model;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public enum QVMType
    {
        [Display(Name = "Multiple Choices")]
        MCQ = 1,
        [Display(Name = "True / False")]
        TrueFalse = 2
    }
    public enum QVMDifficulty { Easy = 1, Medium = 2, Hard = 3 }
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QVMType Type { get; set; }
        public int Grade { get; set; }
        public QVMDifficulty Difficulty { get; set; }
        public int CourseId { get; set; }
        public int? InstructorId { get; set; }      
        public ICollection<ChoiceViewModel> Choices { get; } = new List<ChoiceViewModel>();
    }
}
