using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace DataAccessLibrary.Data
{
    public class ESContext : DbContext
    {

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }

        public ESContext(DbContextOptions options):base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
