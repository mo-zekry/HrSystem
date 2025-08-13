using HrSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrSystem.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedNever();

        builder.Property(e => e.FirstName).HasMaxLength(200).IsRequired();

        builder.Property(e => e.LastName).HasMaxLength(200).IsRequired();

        builder.Property(e => e.Email).HasMaxLength(320).IsRequired();

        builder.Property(e => e.HireDate).IsRequired();

        builder.Property(e => e.Status).HasConversion<int>().IsRequired();

        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate);

        builder.HasIndex(e => e.Email).IsUnique();
        builder.HasIndex(e => e.OrgUnitId);

        builder
            .HasOne(e => e.OrgUnit)
            .WithMany()
            .HasForeignKey(e => e.OrgUnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(e => e.ManagedUnits)
            .WithOne(um => um.Employee)
            .HasForeignKey(um => um.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
