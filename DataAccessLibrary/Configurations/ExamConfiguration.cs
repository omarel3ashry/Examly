using Azure;
using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(e => e.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.ExamDate)
                .IsRequired();

            builder.Property(e => e.DurationInMinutes)
                .IsRequired();

            builder.HasOne(e => e.Course)
                .WithMany(e => e.Exams)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();

            builder.HasMany(e => e.Students)
                .WithMany(builder => builder.Exams)
                .UsingEntity<ExamTaken>();

            builder.HasMany(e => e.Questions)
                          .WithMany(e => e.Exams)
                          .UsingEntity<ExamQuestion>(
                              r => r.HasOne<Question>().WithMany().HasForeignKey(e => e.QuestionId).OnDelete(DeleteBehavior.NoAction),
                              l => l.HasOne<Exam>().WithMany().HasForeignKey(e => e.ExamId).OnDelete(DeleteBehavior.NoAction));
        }
    }
}
