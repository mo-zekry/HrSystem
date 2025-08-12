using HrSystem.Application.Repositories;
using HrSystem.Domain.Abstractions;
using HrSystem.Domain.Specifications;
using HrSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HrSystem.Infrastructure.Repositories;

public class EfRepository<T>(HrSystemDbContext dbContext) : IRepository<T>
    where T : BaseEntity, IAggregateRoot
{
    private readonly HrSystemDbContext _dbContext = dbContext;

    private DbSet<T> Set => _dbContext.Set<T>();

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        // Uses FindAsync for primary key lookup (no includes/specification here by design)
        return await Set.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default
    )
    {
        return await SpecificationEvaluator<T>
            .GetQuery(Set.AsQueryable(), specification)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default
    )
    {
        return await SpecificationEvaluator<T>
            .GetQuery(Set.AsQueryable(), specification)
            .CountAsync(cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Set.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        Set.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // IQueryable and raw SQL exposure
    public IQueryable<T> AsQueryable() => Set.AsQueryable();

    public IQueryable<TQuery> Query<TQuery>()
        where TQuery : class
    {
        // Ensure the type is part of the EF model (can be keyless)
        var entityType = _dbContext.Model.FindEntityType(typeof(TQuery));
        if (entityType is null)
        {
            throw new InvalidOperationException(
                $"Type '{typeof(TQuery).FullName}' is not part of the DbContext model. "
                    + "Register it (HasNoKey for keyless) in OnModelCreating to use Query<T>()."
            );
        }
        return _dbContext.Set<TQuery>().AsQueryable();
    }

    public IQueryable<TQuery> FromSql<TQuery>(FormattableString sql)
        where TQuery : class
    {
        // Requires TQuery to be mapped (regular or keyless). See EF Core docs on FromSql/Keyless types.
        var entityType = _dbContext.Model.FindEntityType(typeof(TQuery));
        if (entityType is null)
        {
            throw new InvalidOperationException(
                $"Type '{typeof(TQuery).FullName}' is not part of the DbContext model. "
                    + "Register it (HasNoKey for keyless) in OnModelCreating to run FromSql<T>()."
            );
        }
        return _dbContext.Set<TQuery>().FromSql(sql);
    }

    public async Task<IReadOnlyList<TQuery>> FromSqlToListAsync<TQuery>(
        FormattableString sql,
        CancellationToken cancellationToken = default
    )
        where TQuery : class
    {
        return await FromSql<TQuery>(sql).AsNoTracking().ToListAsync(cancellationToken);
    }
}
