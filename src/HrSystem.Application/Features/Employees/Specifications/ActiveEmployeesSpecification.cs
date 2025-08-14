using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class ActiveEmployeesSpecification : BaseSpecification<Employee>
{
    public ActiveEmployeesSpecification()
        : base(e => e.Status == EmployeeStatus.Active)
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}