using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class DepartmentCourseConfiguration : IEntityTypeConfiguration<DepartmentCourse>
    {
        public void Configure(EntityTypeBuilder<DepartmentCourse> builder)
        {
            builder.HasKey(e => new { e.DepartmentId, e.CourseId });

            builder.HasOne(e => e.Department)
                .WithMany(e => e.DepartmentCourses)
                .HasForeignKey(e => e.DepartmentId)
                .IsRequired();

            builder.HasOne(e => e.Course)
                .WithMany(e => e.DepartmentCourses)
                .HasForeignKey(e => e.CourseId)
                .IsRequired();

            builder.HasOne(e => e.Instructor)
                .WithMany(e => e.DepartmentCourses)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
