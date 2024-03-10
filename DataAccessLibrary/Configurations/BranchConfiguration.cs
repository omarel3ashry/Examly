using DataAccessLibrary.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLibrary.ModelValidators
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(e => e.Location)
                .HasMaxLength(300)
                .IsRequired();

            builder.HasMany(e => e.Departments)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .IsRequired();

            builder.HasMany(e => e.Instructors)
                .WithOne(e => e.Branch)
                .HasForeignKey(e => e.BranchId)
                .IsRequired();
        }
    }
}
