using Microsoft.EntityFrameworkCore;
using onlineExamInestractour.Models;


namespace onlineExamInestractour.Repository
{
    public interface IExamRepo
    {
        public void AddExam(Exam exam);
        public void AddExamQuestions(int Examid, int CrsId);
        public List<ExamQuestions> ShowRandomQuestions(int Examid);


        public class ExamRepo : IExamRepo
        {
            private  ExamDbContext context;

            public ExamRepo(ExamDbContext _context)
            {
                context= _context;
            }

            public void AddExam(Exam exam)
            {
                context.Exams.Add(exam);
                context.SaveChanges();
            }

            public void AddExamQuestions(int examId, int courseId)
            {
                var random = new Random();

                
                var questionIds = context.Questions
                    .Where(q => q.CourseId == courseId )
                    .Select(q => q.QuestionId)
                    .ToList();

                
                questionIds = questionIds.OrderBy(q => random.Next()).ToList();

                var numQuestionsToAdd = 3; 
                var selectedQuestionIds = questionIds.Take(numQuestionsToAdd);

               
                foreach (var questionId in selectedQuestionIds)
                {
                    var examQuestion = new ExamQuestions
                    {
                        ExamId = examId,
                        QuestionId = (int)questionId
                        
                    };
                    context.ExamsQuestions.Add(examQuestion);
                }

                context.SaveChanges();
            }

            public List<ExamQuestions> ShowRandomQuestions(int examId)
            {
                return context.ExamsQuestions
                    .Where(eq => eq.ExamId == examId)
                    .Include(eq => eq.Question.Choices)
                    
                    .ToList();
            }

            
        }

    }
}
