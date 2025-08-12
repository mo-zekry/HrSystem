using HrSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrSystem.Infrastructure.Persistence.Configurations;

public class OrgTypeConfiguration : IEntityTypeConfiguration<OrgType>
{
    public void Configure(EntityTypeBuilder<OrgType> builder)
    {
        builder.ToTable("org_types");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.Name).HasMaxLength(200).IsRequired();

        builder.Property(o => o.CreatedDate).IsRequired();
        builder.Property(o => o.UpdatedDate);

        builder.HasIndex(o => o.Name).IsUnique();
    }
}
