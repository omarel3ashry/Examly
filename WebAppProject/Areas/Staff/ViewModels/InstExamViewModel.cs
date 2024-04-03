using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using WebAppProject.ViewModels;

namespace WebAppProject.Areas.Staff.ViewModels
{
    public class InstExamViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }

        [MinLength(5, ErrorMessage = "Title should be at least 5 characters.")]
        public string Title { get; set; }

        [Range(5, 360, ErrorMessage = "Duration should be at least 5 minutes.")]
        public int DurationInMinutes { get; set; }

        [DisplayFormat(DataFormatString = "yyyy-MM-ddTHH:mm", ApplyFormatInEditMode = true)]
        public DateTime ExamDate { get; set; }

        [ValidateNever]
        public int[] TotalMCQ { get; set; }

        [ValidateNever]
        public int[] NoOfMCQ { get; set; }

        [ValidateNever]
        public int[] TotalTF { get; set; }

        [ValidateNever]
        public int[] NoOfTF { get; set; }

        public int TotalGrade { get; set; }

        public string? CourseName { get; set; }
        public List<QuestionViewModel>? Questions { get; set; }

        public InstExamViewModel()
        {
            Title = string.Empty;
            ExamDate = DateTime.Now;
        }
    }
}