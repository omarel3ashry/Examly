using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class ChoiceConfiguration : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.Property(e => e.Text)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.IsCorrect)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasOne(e => e.Question)
                .WithMany(e => e.Choices)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
