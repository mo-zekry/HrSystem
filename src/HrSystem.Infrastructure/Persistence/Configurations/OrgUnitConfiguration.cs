using HrSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrSystem.Infrastructure.Persistence.Configurations;

public class OrgUnitConfiguration : IEntityTypeConfiguration<OrgUnit>
{
    public void Configure(EntityTypeBuilder<OrgUnit> builder)
    {
        builder.ToTable("org_units");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.Name).HasMaxLength(200).IsRequired();

        builder.Property(o => o.CreatedDate).IsRequired();
        builder.Property(o => o.UpdatedDate);

        // Relations
        builder
            .HasOne<OrgType>()
            .WithMany()
            .HasForeignKey(o => o.OrgTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<OrgUnit>()
            .WithMany()
            .HasForeignKey(o => o.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Employee>()
            .WithMany()
            .HasForeignKey(o => o.ManagerId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(x => new { x.Name, x.OrgTypeId }).IsUnique();
    }
}
