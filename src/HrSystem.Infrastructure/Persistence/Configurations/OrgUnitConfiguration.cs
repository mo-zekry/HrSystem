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
            .HasOne(o => o.OrgType)
            .WithMany(ot => ot.OrgUnits)
            .HasForeignKey(o => o.OrgTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(o => o.Parent)
            .WithMany(o => o.Children)
            .HasForeignKey(o => o.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(o => o.Managers)
            .WithOne(um => um.OrgUnit)
            .HasForeignKey(um => um.OrgUnitId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => new { x.Name, x.OrgTypeId }).IsUnique();
    }
}
