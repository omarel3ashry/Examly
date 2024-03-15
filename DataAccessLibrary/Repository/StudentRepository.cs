using DataAccessLibrary.Data;
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

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
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
            entity.Address = "ba7riiiii";
            entity.Email = "omar";
            entity.Gender = "f";
            entity.Phone = "23453245";
            
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
            var student = _context.Students.Find(id);
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
            return _context.Students.Where(predicate).ToList();
        }

        public Task<List<Student>> SelectAllAsync(Expression<Func<Student, bool>> predicate)
        {
            return _context.Students.Where(predicate).ToListAsync();
        }

        public List<ExamChoices> GetStudentAnswers(int studentId, int examId)
        {
            var student = _context.Students
                .Include(e => e.StudentAnswers.Where(e => e.ExamId == examId))
                .ThenInclude(e => e.Choice)
                .ThenInclude(e => e.Question)
                .FirstOrDefault(e => e.Id == studentId);

            var questionCorrectChoices = new List<Choice>();
            var studentChoicesForQuestion = new List<Choice>();
            var result = new List<ExamChoices>();
            bool isQuestionCorrect;
            if (student != null)
            {
                var listOfChoices = student.StudentAnswers.Select(e => e.Choice);
                foreach (var choice in listOfChoices)
                {
                    if (!result.Select(r => r.Question).Contains(choice.Question))
                    {
                        Question question = choice.Question;
                        questionCorrectChoices = _context.Choices.Where(e => e.QuestionId == question.Id && e.IsCorrect).ToList();
                        studentChoicesForQuestion = listOfChoices.Where(e => e.QuestionId == question.Id).ToList();
                        isQuestionCorrect=  questionCorrectChoices.Count == studentChoicesForQuestion.Count 
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
            }
            return result;
        }
        public void AddStudentAnswers(int examId, int studentId, List<Choice> choices)
        {
            
            foreach (Choice choice in choices)
            {
                StudentAnswer studentAnswer = new StudentAnswer { ExamId = examId, StudentId = studentId,ChoiceId=choice.Id };
                _context.StudentAnswers.Add(studentAnswer);
            }
            _context.SaveChanges();
        }
    }
}
