using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
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

            builder.HasOne(e => e.Branch)
                .WithMany(e => e.Instructors)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.ManagedDepartment)
                .WithOne(e => e.Manager)
                .HasForeignKey<Department>(e => e.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(e => e.Questions)
                .WithOne(e => e.Instructor)
                .HasForeignKey(e => e.InstructorId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(e => e.User)
            .WithOne(e => e.Instructor)
            .HasForeignKey<Instructor>(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
