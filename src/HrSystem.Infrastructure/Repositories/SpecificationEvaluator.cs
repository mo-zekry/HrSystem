using HrSystem.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace HrSystem.Infrastructure.Repositories;

public static class SpecificationEvaluator<T>
    where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        if (inputQuery is null)
            throw new ArgumentNullException(nameof(inputQuery));
        if (specification is null)
            throw new ArgumentNullException(nameof(specification));

        IQueryable<T> query = inputQuery;

        // Filtering
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes
        if (specification.Includes is { Count: > 0 } includes)
        {
            specification.Includes.Aggregate(query, (current, include) => current.Include(include));
        }

        // Ordering
        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        // Paging
        if (specification.Skip.HasValue && specification.Skip.Value > 0)
        {
            query = query.Skip(specification.Skip.Value);
        }

        if (specification.Take.HasValue && specification.Take.Value > 0)
        {
            query = query.Take(specification.Take.Value);
        }

        // AsNoTracking is an EF Core-specific behavior, so we apply it here
        if (specification.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}
