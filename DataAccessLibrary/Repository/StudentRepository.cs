using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ESContext _context;

        public StudentRepository(ESContext context)
        {
            _context = context;
        }
        public List<Student> GetAll()
        {
            return _context.Students.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _context.Students.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Student> GetAllWithIncludes()
        {
            return _context.Students
                .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .ToList();
        }

        public Task<List<Student>> GetAllWithIncludesAsync()
        {
            return _context.Students
                .Where(e => !e.IsDeleted)
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .ToListAsync();
        }

        public Student? GetById(int id)
        {
            return _context.Students.Find(id);
        }

        public ValueTask<Student?> GetByIdAsync(int id)
        {
            return _context.Students.FindAsync(id);
        }

        public Student? GetByIdWithIncludes(int id)
        {
            return _context.Students
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Student?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Students
                .Include(e => e.User)
                .Include(e => e.Department)
                .Include(e => e.Exams)
                .Include(e => e.StudentAnswers)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public Student? GetByUserId(int userId)
        {
            return _context.Students.FirstOrDefault(e => e.UserId == userId);
        }

        public Task<Student?> GetByUserIdAsync(int userId)
        {
            return _context.Students.FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public int Add(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Student entity)
        {
            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Student entity)
        {
            if (entity != null)
            {
                _context.Students.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Student entity)
        {
            if (entity != null)
            {
                _context.Students.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                student.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                student.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Student? Select(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).FirstOrDefault();
        }

        public Task<Student?> SelectAsync(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).FirstOrDefaultAsync();
        }

        public List<Student> SelectAll(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(e => !e.IsDeleted).Where(predicate).ToList();
        }

        public Task<List<Student>> SelectAllAsync(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(e => !e.IsDeleted).Where(predicate).ToListAsync();
        }

        public async Task<List<ExamChoices>> GetStudentAnswersAsync(int studentId, int examId)
        {
            var student = await _context.Students
                .Include(e => e.StudentAnswers.Where(e => e.ExamId == examId))
                .ThenInclude(e => e.Choice)
                .ThenInclude(e => e.Question)
                .FirstOrDefaultAsync(e => e.Id == studentId);
            var exam = await _context.Exams
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.Id == examId);
            var answeredQuestions = new List<Question>();
            var questionCorrectChoices = new List<Choice>();
            var studentChoicesForQuestion = new List<Choice>();
            var result = new List<ExamChoices>();
            bool isQuestionCorrect;
            if (student != null && exam!=null)
            {
                var listOfChoices = student.StudentAnswers.Select(e => e.Choice);
                foreach (var choice in listOfChoices)
                {
                    if (!result.Select(r => r.Question).Contains(choice.Question))
                    {
                        Question question = choice.Question;
                        answeredQuestions.Add(question);
                        questionCorrectChoices = await _context.Choices.Where(e => e.QuestionId == question.Id && e.IsCorrect).ToListAsync();
                        studentChoicesForQuestion = listOfChoices.Where(e => e.QuestionId == question.Id).ToList();
                        isQuestionCorrect = questionCorrectChoices.Count == studentChoicesForQuestion.Count
                                         && questionCorrectChoices.Count == studentChoicesForQuestion.FindAll(e => e.IsCorrect).Count;
                        result.Add(new ExamChoices
                        {
                            Question = choice.Question,
                            CorrectChoices = questionCorrectChoices,
                            StudentChoices = studentChoicesForQuestion,
                            IsCorrect = isQuestionCorrect
                        });
                    }
                }
                if (exam.Questions.Count != answeredQuestions.Count)
                {
                    foreach (var question in exam.Questions)
                    {
                        if (!answeredQuestions.Contains(question))
                        {
                            result.Add(new ExamChoices
                            {
                                Question = question,
                                CorrectChoices = await _context.Choices.Where(e => e.QuestionId == question.Id && e.IsCorrect).ToListAsync(),
                                StudentChoices = new List<Choice>(),
                                IsCorrect = false
                            });
                        }
                    }
                }
            }
            return result;
        }

        public void AddStudentAnswers(int examId, int studentId, List<Choice> choices)
        {

            foreach (Choice choice in choices)
            {
                StudentAnswer studentAnswer = new StudentAnswer { ExamId = examId, StudentId = studentId, ChoiceId = choice.Id };
                _context.StudentAnswers.Add(studentAnswer);
            }
            _context.SaveChanges();
        }

        public async Task<bool> AddStudentAnswersAsync(int examId, int studentId, List<Choice> choices)
        {
            var studentAnswers = new List<StudentAnswer>();
            foreach (Choice choice in choices)
            {
                StudentAnswer studentAnswer = new StudentAnswer { ExamId = examId, StudentId = studentId, ChoiceId = choice.Id };
                studentAnswers.Add(studentAnswer);
            }
            await _context.StudentAnswers.AddRangeAsync(studentAnswers);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
