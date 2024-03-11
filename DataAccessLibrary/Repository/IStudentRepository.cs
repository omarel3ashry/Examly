using DataAccessLibrary.Model;
using System.Linq.Expressions;

namespace DataAccessLibrary.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        public List<Student> SelectAll(Expression<Func<Student, bool>> predicate);
    }
}
