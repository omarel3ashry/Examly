using DataAccessLibrary.Model;
using System.ComponentModel.DataAnnotations;

namespace WebAppProject.ViewModels
{
    public class InstExamViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int DurationInMinutes { get; set; }

        [DisplayFormat(DataFormatString = "MM/dd/yyyy hh:mm tt", ApplyFormatInEditMode = true)]
        public DateTime ExamDate { get; set; }
        public QVMDifficulty ExamDifficulty { get; set; }
        public int TotalMCQ { get; set; }
        public int NoOfMCQ { get; set; }
        public int TotalTF { get; set; }
        public int NoOfTF { get; set; }
        public int TotalGrade { get; set; }

        public string? CourseName { get; set; }
        public List<QuestionViewModel>? Questions { get; set; }

        public InstExamViewModel()
        {
            Title = string.Empty;
            ExamDate = DateTime.Now;
        }

        public InstExamViewModel(Exam entity)
        {
            Id = entity.Id;
            CourseId = entity.CourseId;
            ExamDate = entity.ExamDate;
            DurationInMinutes = entity.DurationInMinutes;
            Title = entity.Title ?? string.Empty;
            TotalGrade = entity.TotalGrade ?? 0;
            CourseName = entity.Course?.Name;
            Questions = new List<QuestionViewModel>();
            foreach (var questionDto in entity.Questions)
            {
                Questions.Add(new QuestionViewModel(questionDto));
            }
        }

        public Exam ToExamDTO(bool isNew = true)
        {
            var examDto = new Exam()
            {
                Id = isNew ? 0 : Id,
                CourseId = CourseId,
                ExamDate = ExamDate,
                DurationInMinutes = DurationInMinutes,
                Title = Title ?? string.Empty,
                TotalGrade = TotalGrade
            };
            if (Questions != null)
            {
                foreach (var question in Questions)
                {
                    examDto.Questions.Add(question.ToQuestionDTO(isNew));
                }
            }

            return examDto;
        }

    }
}