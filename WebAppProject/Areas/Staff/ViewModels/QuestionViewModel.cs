using System.ComponentModel.DataAnnotations;
using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Staff.ViewModels
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
        public int CourseId { get; set; }
        public int? InstructorId { get; set; }

        [MinLength(5, ErrorMessage = "Question should be at least 5 characters.")]
        public string Text { get; set; }
        public QVMType Type { get; set; }

        [Range(1, 30, ErrorMessage = "Grade should be from 1 to 30.")]
        public int Grade { get; set; }
        public QVMDifficulty Difficulty { get; set; }
        public List<ChoiceViewModel> Choices { get; }
        public string? CourseName { get; set; }

        public QuestionViewModel()
        {
            Text = string.Empty;
            Choices = new List<ChoiceViewModel>();
        }
    }
}
