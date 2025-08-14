using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class EmployeesSearchByNameSpecification : BaseSpecification<Employee>
{
    public EmployeesSearchByNameSpecification(string term)
        : base(e => (e.FirstName + " " + e.LastName).Contains(term))
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}
