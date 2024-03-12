using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Age)
                .IsRequired();

            builder.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(e => e.Department)
                .WithMany(e => e.Students)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e=>e.User)
                .WithOne(e => e.Student)
                .HasForeignKey<Student>(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
