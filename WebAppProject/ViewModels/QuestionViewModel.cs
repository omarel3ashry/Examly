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
        public int CourseId { get; set; }
        public int? InstructorId { get; set; }
        public string Text { get; set; }
        public QVMType Type { get; set; }
        public int Grade { get; set; }
        public QVMDifficulty Difficulty { get; set; }
        public List<ChoiceViewModel> Choices { get; }
        public Course? Course { get; set; }

        public QuestionViewModel()
        {
            Text = string.Empty;
            Choices = new List<ChoiceViewModel>();
        }

        public QuestionViewModel(Question entity)
        {
            Id = entity.Id;
            CourseId = entity.CourseId;
            InstructorId = entity.InstructorId;
            Text = entity.Text ?? string.Empty;
            Type = (QVMType)(int)entity.Type;
            Grade = entity.Grade;
            Difficulty = (QVMDifficulty)(int)entity.Difficulty;
            Choices = new List<ChoiceViewModel>();
            Course = entity.Course;
            foreach (var choiceDto in entity.Choices)
            {
                Choices.Add(new ChoiceViewModel()
                {
                    Id = choiceDto.Id,
                    QuestionId = choiceDto.QuestionId,
                    Text = choiceDto.Text,
                    IsCorrect = choiceDto.IsCorrect
                });
            }
        }

        public Question ToQuestionDTO(bool isNew = true)
        {
            var questionDto = new Question()
            {
                Id = isNew ? 0 : Id,
                CourseId = CourseId,
                InstructorId = InstructorId,
                Text = Text,
                Type = (QType)(int)Type,
                Grade = Grade,
                Difficulty = (QDifficulty)(int)Difficulty,
            };
            if (Choices.Count > 2)
            {
                foreach (var choice in Choices)
                {
                    questionDto.Choices.Add(new Choice()
                    {
                        Id = isNew ? 0 : choice.Id,
                        QuestionId = choice.QuestionId,
                        Text = choice.Text,
                        IsCorrect = choice.IsCorrect
                    });
                }
            }
            else
            {
                questionDto.Choices.Add(new Choice()
                {
                    Id = isNew ? 0 : Choices[0].Id,
                    QuestionId = Choices[0].QuestionId,
                    Text = Choices[0].Text,
                    IsCorrect = true
                });

                questionDto.Choices.Add(new Choice()
                {
                    Id = isNew ? 0 : Choices[1].Id,
                    QuestionId = Choices[0].QuestionId,
                    Text = Choices[0].Text.Equals("True") ? "False" : "True",
                    IsCorrect = false
                }); ;
            }
            return questionDto;
        }

    }
}
