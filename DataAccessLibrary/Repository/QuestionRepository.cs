using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ESContext _context;

        public QuestionRepository(ESContext context)
        {
            _context = context;
        }
        public List<Question> GetAll()
        {
            return _context.Questions.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Question>> GetAllAsync()
        {
            return _context.Questions.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Question> GetAllWithIncludes()
        {
            return _context.Questions
                .Where(e => !e.IsDeleted)
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Choices)
                .Include(e => e.Exams)
                .ToList();
        }

        public Task<List<Question>> GetAllWithIncludesAsync()
        {
            return _context.Questions
                .Where(e => !e.IsDeleted)
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Choices)
                .Include(e => e.Exams)
                .ToListAsync();
        }

        public Question? GetById(int id)
        {
            return _context.Questions.Find(id);
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public Question? GetByIdWithIncludes(int id)
        {
            return _context.Questions
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Choices)
                .Include(e => e.Exams)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Question?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Questions
                .Include(e => e.Course)
                .Include(e => e.Instructor)
                .Include(e => e.Choices)
                .Include(e => e.Exams)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Question entity)
        {
            _context.Questions.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Question entity)
        {
            _context.Questions.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Question entity)
        {
            if (entity != null)
            {
                _context.Questions.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Question entity)
        {
            if (entity != null)
            {
                _context.Questions.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var question = _context.Questions.Find(id);
            if (question != null)
            {
                question.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var question = _context.Questions.Find(id);
            if (question != null)
            {
                question.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Question? Select(Expression<Func<Question, bool>> predicate)
        {
            return _context.Questions.Where(predicate).FirstOrDefault();
        }

        public Task<Question?> SelectAsync(Expression<Func<Question, bool>> predicate)
        {
            return _context.Questions.Where(predicate).FirstOrDefaultAsync();
        }

        public bool AddChoice(Choice choice)
        {
            _context.Choices.Add(choice);
            return _context.SaveChanges() == 1;
        }

        public async Task<bool> AddChoiceAsync(Choice choice)
        {
            _context.Choices.Add(choice);
            return await _context.SaveChangesAsync() == 1;
        }

        public int AddChoices(List<Choice> choices)
        {
            _context.Choices.AddRange(choices);
            return _context.SaveChanges();
        }

        public Task<int> AddChoicesAsync(List<Choice> choices)
        {
            _context.Choices.AddRange(choices);
            return _context.SaveChangesAsync();
        }

        public List<Question> SelectAll(Expression<Func<Question, bool>> predicate)
        {
            return _context.Questions.Where(predicate).ToList();
        }

        public Task<List<Question>> SelectAllAsync(Expression<Func<Question, bool>> predicate)
        {
            return _context.Questions.Where(predicate).ToListAsync();
        }
    }
}
