using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class ChoiceRepository : IChoiceRepository
    {
        private readonly ESContext _context;

        public ChoiceRepository(ESContext context)
        {
            _context = context;
        }
        public List<Choice> GetAll()
        {
            return _context.Choices.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Choice>> GetAllAsync()
        {
            return _context.Choices.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Choice> GetAllWithIncludes()
        {
            return _context.Choices
                .Where(e => !e.IsDeleted)
                .Include(e => e.Question)
                .Include(e => e.StudentAnswers)
                .ToList();
        }

        public Task<List<Choice>> GetAllWithIncludesAsync()
        {
            return _context.Choices
                .Where(e => !e.IsDeleted)
                .Include(e => e.Question)
                .Include(e => e.StudentAnswers)
                .ToListAsync();
        }

        public Choice? GetById(int id)
        {
            return _context.Choices.Find(id);
        }

        public async Task<Choice?> GetByIdAsync(int id)
        {
            return await _context.Choices.FindAsync(id);
        }

        public Choice? GetByIdWithIncludes(int id)
        {
            return _context.Choices
                .Include(e => e.Question)
                .Include(e => e.StudentAnswers)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Choice?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Choices
                .Include(e => e.Question)
                .Include(e => e.StudentAnswers)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Choice entity)
        {
            _context.Choices.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Choice entity)
        {
            _context.Choices.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Choice entity)
        {
            if (entity != null)
            {
                _context.Choices.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Choice entity)
        {
            if (entity != null)
            {
                _context.Choices.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var choice = _context.Choices.Find(id);
            if (choice != null)
            {
                choice.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var choice = _context.Choices.Find(id);
            if (choice != null)
            {
                choice.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Choice? Select(Expression<Func<Choice, bool>> predicate)
        {
            return _context.Choices.Where(predicate).FirstOrDefault();
        }

        public Task<Choice?> SelectAsync(Expression<Func<Choice, bool>> predicate)
        {
            return _context.Choices.Where(predicate).FirstOrDefaultAsync();
        }

        List<Choice> IRepository<Choice>.SelectAll(Expression<Func<Choice, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
