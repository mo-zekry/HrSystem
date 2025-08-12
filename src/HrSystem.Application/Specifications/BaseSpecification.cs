using System.Linq.Expressions;
using HrSystem.Domain.Specifications;

namespace HrSystem.Application.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    protected BaseSpecification() { }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    // ISpecification implementation
    public Expression<Func<T, bool>>? Criteria { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int? Skip { get; private set; }
    public int? Take { get; private set; }

    public bool AsNoTracking { get; private set; }

    // Fluent builders (protected) for use by derived specifications
    protected void AddInclude(Expression<Func<T, object>> includeExpression) =>
        Includes.Add(includeExpression);

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
        OrderBy = orderByExpression;

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression) =>
        OrderByDescending = orderByDescExpression;

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }

    protected void EnableNoTracking() => AsNoTracking = true;

    protected void Where(Expression<Func<T, bool>> criteria) => Criteria = criteria;
}
