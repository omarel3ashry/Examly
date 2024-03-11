using DataAccessLibrary.Data;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ESContext _context;

        public BranchRepository(ESContext context)
        {
            _context = context;
        }
        public List<Branch> GetAll()
        {
            return _context.Branches.Where(e => !e.IsDeleted).ToList();
        }

        public Task<List<Branch>> GetAllAsync()
        {
            return _context.Branches.Where(e => !e.IsDeleted).ToListAsync();
        }
        public List<Branch> GetAllWithIncludes()
        {
            return _context.Branches
                .Where(e => !e.IsDeleted)
                .Include(e => e.Departments)
                .Include(e => e.Instructors)
                .ToList();
        }

        public Task<List<Branch>> GetAllWithIncludesAsync()
        {
            return _context.Branches
                .Where(e => !e.IsDeleted)
                .Include(e => e.Departments)
                .Include(e => e.Instructors)
                .ToListAsync();
        }

        public Branch? GetById(int id)
        {
            return _context.Branches.Find(id);
        }

        public async Task<Branch?> GetByIdAsync(int id)
        {
            return await _context.Branches.FindAsync(id);
        }

        public Branch? GetByIdWithIncludes(int id)
        {
            return _context.Branches
                .Include(e => e.Departments)
                .Include(e => e.Instructors)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task<Branch?> GetByIdWithIncludesAsync(int id)
        {
            return _context.Branches
                .Include(e => e.Departments)
                .Include(e => e.Instructors)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public int Add(Branch entity)
        {
            _context.Branches.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(Branch entity)
        {
            _context.Branches.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public bool Update(Branch entity)
        {
            if (entity != null)
            {
                _context.Branches.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(Branch entity)
        {
            if (entity != null)
            {
                _context.Branches.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var branch = _context.Branches.Find(id);
            if (branch != null)
            {
                branch.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var branch = _context.Branches.Find(id);
            if (branch != null)
            {
                branch.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Branch? Select(Expression<Func<Branch, bool>> predicate)
        {
            return _context.Branches.Where(predicate).FirstOrDefault();
        }

        public Task<Branch?> SelectAsync(Expression<Func<Branch, bool>> predicate)
        {
            return _context.Branches.Where(predicate).FirstOrDefaultAsync();
        }

        List<Branch> IRepository<Branch>.SelectAll(Expression<Func<Branch, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
