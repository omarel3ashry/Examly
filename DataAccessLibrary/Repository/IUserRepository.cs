using DataAccessLibrary.Model;

namespace DataAccessLibrary.Repository
{
    public interface IUserRepository
    {
        public User? CheckUser(string email, string password);
        public Task<User?> CheckUserAsync(string email, string password);
        public User? GetByEmail(string email);
        public Task<User?> GetByEmailAsync(string email);
        public int Add(User entity);
        public Task<int> AddAsync(User entity);
        public bool Update(User entity);
        public Task<bool> UpdateAsync(User entity);

    }
}
