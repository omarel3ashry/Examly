using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class StudentAnswerConfiguration : IEntityTypeConfiguration<StudentAnswer>
    {
        public void Configure(EntityTypeBuilder<StudentAnswer> builder)
        {
            builder.HasKey(e => new { e.StudentId, e.ExamId, e.ChoiceId });

            builder.HasOne(e => e.Student)
                .WithMany(e => e.StudentAnswers)
                .HasForeignKey(e => e.StudentId)
                .IsRequired(false);

            builder.HasOne(e => e.Choice)
                .WithMany(e => e.StudentAnswers)
                .HasForeignKey(e => e.ChoiceId)
                .IsRequired();

            builder.HasOne(e => e.Exam)
                .WithMany(e => e.StudentAnswers)
                .HasForeignKey(e => e.ExamId)
                .IsRequired(false);
        }
    }
}
