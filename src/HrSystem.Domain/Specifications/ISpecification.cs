using System.Linq.Expressions;

namespace HrSystem.Domain.Specifications;

public interface ISpecification<T>
{
    // Filtering
    Expression<Func<T, bool>>? Criteria { get; }

    // Include expressions
    List<Expression<Func<T, object>>> Includes { get; }

    // Ordering
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    // Paging
    int? Skip { get; }
    int? Take { get; }
    
    bool AsNoTracking { get; }
}
