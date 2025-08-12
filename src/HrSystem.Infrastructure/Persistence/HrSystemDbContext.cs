using HrSystem.Domain.Entities;
using HrSystem.Domain.Abstractions;
using HrSystem.Application.OrgUnits.Queries;
using Microsoft.EntityFrameworkCore;

namespace HrSystem.Infrastructure.Persistence;

public class HrSystemDbContext : DbContext
{
    public HrSystemDbContext(DbContextOptions<HrSystemDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<OrgUnit> OrgUnits => Set<OrgUnit>();
    public DbSet<OrgType> OrgTypes => Set<OrgType>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<T> in this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrSystemDbContext).Assembly);

    // Register keyless projection types used by Application queries
    modelBuilder.Entity<OrgUnitFlat>().HasNoKey();
    }

    public override int SaveChanges()
    {
        ApplyAuditTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyAuditTimestamps()
    {
        var utcNow = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.UpdatedDate).CurrentValue = utcNow;
            }
            else if (entry.State == EntityState.Added)
            {
                // Ensure CreatedDate is set (BaseEntity constructor should do it, but keep safe)
                if (entry.Entity.CreatedDate == default)
                    entry.Property(e => e.CreatedDate).CurrentValue = utcNow;
            }
        }
    }
}
