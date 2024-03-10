using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace DataAccessLibrary.Data
{
    public class ESContext : DbContext
    {

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<DepartmentCourse> DepartmentCourses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamTaken> ExamsTaken { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        public ESContext(DbContextOptions options):base(options) {}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
