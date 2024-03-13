/*using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using NuGet.Protocol.Core.Types;
using WebAppProject.Models;
using WebAppProject.ViewModels;

namespace WebAppProject.Repository
{
    public interface IInstructorRepository
    {
        IEnumerable<Instructor> GetInstructors();
        Instructor GetInstructorById(int instructorId);
        IEnumerable<InstructorViewModel> GetCoursesTaught();
        CourseInfo GetCourseInfo(int courseId);
        IEnumerable<Question> GetQuestionBank(int courseId);
        void UpdateQuestion(Question question, IEnumerable<Choice> choices);
        Question GetQuestionById(int id);
        ICollection<Choice> GetChoicesForQuestion(int questionId);
        void AddQuestion(Question question);
        void AddQuestionWithChoices(Question question, List<string> choiceTexts);
        void Deletequestion(int id);
         List<Question> GetAllQuestions();
        List<Question> GetRandomQuestions(int count);
        QuestionInfo GetQuestionInfo(int QuestionId);
        IEnumerable<Course> GetCourses();
        Course GetCourseById(int Id);
         void SaveChanges();
         Detialsviewmodel GetInfo(int CourseId);
         void AddCourse(Course course);
        void Deletecourse(int id);
      
    }
    public class InstructorRepo : IInstructorRepository
    {
        ExamDbContext context;

        public InstructorRepo(ExamDbContext _context)
        {
            context = _context;
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public IEnumerable<Instructor> GetInstructors()
        {
            return context.Instructors.ToList();
        }

        public Instructor GetInstructorById(int Id)
        {
            return context.Instructors.FirstOrDefault(i => i.InstructorId == Id);
        }

        public IEnumerable<InstructorViewModel> GetCoursesTaught()
        {
            return context.Courses

                 .Select(c => new InstructorViewModel
                 {
                     DepartmentName = c.Department.DepartmentName,
                     CourseName = c.CourseName,
                     CourseId = c.CourseId
                 })
                 .ToList();
        }
        public IEnumerable<Course> GetCourses()
        {
            return context.Courses
                 .ToList();
        }
        public Course GetCourseById(int Id)
        {
            return context.Courses.FirstOrDefault(c => c.CourseId == Id);
        }
        public CourseInfo GetCourseInfo(int courseId)
        {
            var course = context.Courses
                .Where(c => c.CourseId == courseId)
                .Select(c => new CourseInfo
                {
                    DepartmentName = c.Department.DepartmentName,
                    CourseName = c.CourseName,

                })
                .FirstOrDefault();

            return course;
        }
        public IEnumerable<Question> GetQuestionBank(int Id)
        {
            return context.Questions
                .Where(q => q.CourseId == Id)
                .ToList();
        }
        public ICollection<Choice> GetChoicesForQuestion(int questionId)
        {

            return context.Choices.Where(c => c.QuestionId == questionId).ToList();
        }
        public List<Question> GetAllQuestions()
        {
            return context.Questions.ToList();
        }

       
        public Question GetQuestionById(int id)
        {
            return context.Questions.FirstOrDefault(q => q.QuestionId == id);
        }
        public void AddQuestion(Question question)
        {

            context.Questions.Add(question);
            context.SaveChanges();
        }
        public void AddQuestionWithChoices(Question question, List<string> choiceTexts)
        {
          // question.CourseId = id;
            var choices = new List<Choice>();

            // Create Choice objects from the choiceTexts
            foreach (var choiceText in choiceTexts)
            {
                var choice = new Choice { Body = choiceText  };
                choices.Add(choice);

            }

            // Assign the choices to the question
            question.Choices = choices;
            context.Questions.Add(question);
            context.SaveChanges();
            

        }

        public void UpdateQuestion(Question question, IEnumerable<Choice> choices)
        {
            var existingQuestion = context.Questions.Include(q => q.Choices).FirstOrDefault(q => q.QuestionId == question.QuestionId);
            if (existingQuestion != null)
            {
                existingQuestion.QuestionText = question.QuestionText;
                existingQuestion.Type = question.Type;
                existingQuestion.Difficulty = question.Difficulty;
                existingQuestion.Grade = question.Grade;


                foreach (var choice in choices)
                {
                    var existingChoice = existingQuestion.Choices.FirstOrDefault(c => c.ChoiceId == choice.ChoiceId);
                    if (existingChoice != null)
                    {
                        existingChoice.Body = choice.Body;
                        existingChoice.IsCorrect = choice.IsCorrect;
                    }
                    else
                    {
                        existingQuestion.Choices.Add(choice);
                    }
                }


                context.SaveChanges();
            }

        }
        public QuestionInfo GetQuestionInfo(int QuestionId)
        {
            var Question = context.Questions
                .Where(c => c.QuestionId == QuestionId)
                .Select(c => new QuestionInfo
                {
                    CourseName = c.Course.CourseName,
                    QuestionText = c.QuestionText,
                    Type=c.Type,
                    Choices=c.Choices


                })
                .FirstOrDefault();

            return Question;
        }
        public Detialsviewmodel GetInfo(int CourseId)
        {
            var Course = context.Courses
                .Where(c => c.CourseId == CourseId)
                .Select(c => new Detialsviewmodel
                {
                    CourseName = c.CourseName,
                    CourseId = c.CourseId,
                    InstructorName = c.Instructor.InstructorName,


                })
                .FirstOrDefault();

            return Course;
        }
        public void Deletequestion(int id)
        {
            // Retrieve the question entity from the database
            var questionToDelete = context.Questions.FirstOrDefault(q => q.QuestionId == id);

            if (questionToDelete != null)
            {

                context.Questions.Remove(questionToDelete);
                context.SaveChanges();

            }
        }
        public void Deletecourse(int id)
        {
            // Retrieve the question entity from the database
            var courseToDelete = context.Courses.FirstOrDefault(q => q.CourseId == id);

            if (courseToDelete != null)
            {

                context.Courses.Remove(courseToDelete);
                context.SaveChanges();

            }
        }
        public void AddCourse(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public List<Question> GetRandomQuestions(int count)
        {
            throw new NotImplementedException();
        }
    }
}
    
*/