using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public Task<List<T>> GetAllAsync();
        public List<T> GetAllWithIncludes();
        public Task<List<T>> GetAllWithIncludesAsync();
        public T? GetById(int id);
        public Task<T?> GetByIdAsync(int id);
        public T? GetByIdWithIncludes(int id);
        public Task<T?> GetByIdWithIncludesAsync(int id);
        public int Add(T entity);
        public Task<int> AddAsync(T entity);
        public bool Update(T entity);
        public Task<bool> UpdateAsync(T entity);
        public bool Delete(int id);
        public Task<bool> DeleteAsync(int id);
        public T? Select(Expression<Func<T, bool>> predicate);
        public Task<T?> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}
