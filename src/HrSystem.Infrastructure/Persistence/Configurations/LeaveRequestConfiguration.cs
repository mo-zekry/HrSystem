using HrSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HrSystem.Infrastructure.Persistence.Configurations;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.ToTable("leave_requests");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).ValueGeneratedNever();

        builder.Property(l => l.Reason).HasMaxLength(1000);

        builder.Property(l => l.Status).HasConversion<int>().IsRequired();

        builder.Property(l => l.CreatedDate).IsRequired();
        builder.Property(l => l.UpdatedDate);

        // Owned value object mapping for DateRange -> columns start_date, end_date
        builder.OwnsOne(
            l => l.Period,
            nav =>
            {
                nav.Property(p => p.Start).HasColumnName("start_date").IsRequired();
                nav.Property(p => p.End).HasColumnName("end_date").IsRequired();
            }
        );

        builder.HasIndex(l => new { l.EmployeeId, l.Status });
        builder.HasIndex(l => new { l.EmployeeId, l.CreatedDate });

        builder
            .HasOne(l => l.Employee)
            .WithMany()
            .HasForeignKey(l => l.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
