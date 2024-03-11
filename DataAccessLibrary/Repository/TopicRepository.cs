using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly ESContext _context;

        public TopicRepository(ESContext context)
        {
            _context = context;
        }
        public List<Topic> GetAll()
        {
            return _context.Topics.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Topic>> GetAllAsync()
        {
            return _context.Topics.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Topic> GetAllWithIncludes()
        {
            return _context.Topics
                .Where(e => !e.IsDeleted)
                .Include(e => e.Courses)
                .ToList();
        }

        public Task<List<Topic>> GetAllWithIncludesAsync()
        {
            return _context.Topics
                .Where(e => !e.IsDeleted)
                .Include(e => e.Courses)
                .ToListAsync();
        }

        public Topic? GetById(int id)
        {
            return _context.Topics.Find(id);
        }

        public async Task<Topic?> GetByIdAsync(int id)
        {
            return await _context.Topics.FindAsync(id);
        }

        public Topic? GetByIdWithIncludes(int id)
        {
            return _context.Topics
                .Include(e => e.Courses)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Topic?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Topics
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Topic entity)
        {
            _context.Topics.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Topic entity)
        {
            _context.Topics.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Topic entity)
        {
            if (entity != null)
            {
                _context.Topics.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Topic entity)
        {
            if (entity != null)
            {
                _context.Topics.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Topic? Select(Expression<Func<Topic, bool>> predicate)
        {
            return _context.Topics.Where(predicate).FirstOrDefault();
        }

        public Task<Topic?> SelectAsync(Expression<Func<Topic, bool>> predicate)
        {
            return _context.Topics.Where(predicate).FirstOrDefaultAsync();
        }

        public List<Topic> SelectAll(Expression<Func<Topic, bool>> predicate)
        {
            return _context.Topics.Where(predicate).ToList();
        }

        public Task<List<Topic>> SelectAllAsync(Expression<Func<Topic, bool>> predicate)
        {
            return _context.Topics.Where(predicate).ToListAsync();
        }
    }
}
