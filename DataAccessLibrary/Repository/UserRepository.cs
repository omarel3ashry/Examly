using DataAccessLibrary.Data;
using DataAccessLibrary.Interfaces;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ESContext _context;

        public UserRepository(ESContext context)
        {
            _context = context;
        }
        public int Add(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public async Task<int> AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public User? CheckUser(string email, string password)
        {
            return _context.Users.Include(a => a.Role).FirstOrDefault(e => e.Email == email && e.Password == password);
        }

        public Task<User?> CheckUserAsync(string email, string password)
        {
            return _context.Users.Include(a => a.Role).FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }

        public bool Update(User entity)
        {
            if (entity != null)
            {
                _context.Users.Update(entity);
                return _context.SaveChanges() == 1;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            if (entity != null)
            {
                _context.Users.Update(entity);
                return await _context.SaveChangesAsync() == 1;
            }
            return false;
        }
    }
}
