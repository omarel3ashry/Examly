using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(e => e.Text)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(e => e.Text)
                .IsRequired();

            builder.Property(e => e.Grade)
                .HasDefaultValue(2);


            builder.HasMany(e => e.Choices)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .IsRequired();

            builder.HasOne(e => e.Course)
                .WithMany(e => e.Questions)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(e => e.Instructor)
                .WithMany(e => e.Questions)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(e => e.Type)
                .HasDefaultValue(QType.MCQ)
                .HasSentinel(QType.Unspecified);

            builder.Property(e => e.Difficulty)
                .HasDefaultValue(QDifficulty.Easy)
                .HasSentinel(QDifficulty.Unspecified);

        }
    }
}
