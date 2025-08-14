using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class EmployeesHiredInRangeSpecification : BaseSpecification<Employee>
{
    public EmployeesHiredInRangeSpecification(DateOnly start, DateOnly end)
        : base(e => e.HireDate >= start && e.HireDate <= end)
    {
        ApplyOrderBy(e => e.HireDate);
        EnableNoTracking();
    }
}