using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(e => e.Name)
               .HasMaxLength(200)
               .IsRequired();

            builder.Property(e => e.Description)
               .HasMaxLength(600);

            builder.HasMany(e => e.Questions)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();

            builder.HasMany(e => e.Exams)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();
        }
    }
}
