using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(e => e.Branch)
                .WithMany(e => e.Departments)
                .HasForeignKey(e => e.BranchId)
                .IsRequired();

            builder.HasOne(e => e.Manager)
                .WithOne(e => e.ManagedDepartment)
                .HasForeignKey<Department>(e => e.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
