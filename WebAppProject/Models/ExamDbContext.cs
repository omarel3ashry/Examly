using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAppProject.Models
{
    public class ExamDbContext : DbContext
    {
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestions> ExamsQuestions { get; set; }
        public ExamDbContext()
        {

        }
        public ExamDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OnlineExamentionSystem;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExamQuestions>()
                .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            modelBuilder.Entity<ExamQuestions>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.ExamsQuestions)
                .HasForeignKey(eq => eq.ExamId);

            modelBuilder.Entity<ExamQuestions>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.ExamsQuestions)
                .HasForeignKey(eq => eq.QuestionId);
        }
    }
}