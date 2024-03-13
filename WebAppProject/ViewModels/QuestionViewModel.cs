using DataAccessLibrary.Model;

namespace WebAppProject.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int? InstructorId { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public int Grade { get; set; }
        public int Difficulty { get; set; }
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
            Type = entity.Type;
            Grade = entity.Grade;
            Difficulty = entity.Difficulty;
            Choices = new List<ChoiceViewModel>();
            Course = entity.Course;
            foreach (var choice in entity.Choices)
            {
                Choices.Add(new ChoiceViewModel()
                {
                    Id = choice.Id,
                    QuestionId = choice.QuestionId,
                    Text = choice.Text,
                    IsCorrect = choice.IsCorrect
                });
            }
        }

        public Question ToQuestionDTO(bool isNew = true)
        {
            var question = new Question()
            {
                Id = isNew ? 0 : Id,
                CourseId = CourseId,
                InstructorId = InstructorId,
                Text = Text,
                Type = Type,
                Grade = Grade,
                Difficulty = Difficulty,
            };
            if (Choices.Count > 1)
            {
                foreach (var choice in Choices)
                {
                    question.Choices.Add(new Choice()
                    {
                        Id = isNew ? 0 : choice.Id,
                        QuestionId = choice.QuestionId,
                        Text = choice.Text,
                        IsCorrect = choice.IsCorrect
                    });
                }
            }
            else if (Choices.Count == 1)
            {
                question.Choices.Add(new Choice()
                {
                    Id = isNew ? 0 : Choices[0].Id,
                    QuestionId = Choices[0].QuestionId,
                    Text = Choices[0].Text,
                    IsCorrect = Choices[0].Text.Equals("True")
                });
            }
            return question;
        }

    }
}
