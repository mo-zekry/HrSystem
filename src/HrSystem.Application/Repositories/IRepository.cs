using HrSystem.Domain.Abstractions;
using HrSystem.Domain.Specifications;

namespace HrSystem.Application.Repositories;

public interface IRepository<T>
    where T : BaseEntity, IAggregateRoot
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    // All list/count methods accept a specification
    Task<IReadOnlyList<T>> ListAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default
    );
    Task<int> CountAsync(
        ISpecification<T> specification,
        CancellationToken cancellationToken = default
    );

    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    IQueryable<T> AsQueryable();
    IQueryable<TQuery> Query<TQuery>()
        where TQuery : class;
    IQueryable<TQuery> FromSql<TQuery>(FormattableString sql)
        where TQuery : class;
    Task<IReadOnlyList<TQuery>> FromSqlToListAsync<TQuery>(
        FormattableString sql,
        CancellationToken cancellationToken = default
    )
        where TQuery : class;
}
