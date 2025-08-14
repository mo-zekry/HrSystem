using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class EmployeesByOrgUnitSpecification : BaseSpecification<Employee>
{
    public EmployeesByOrgUnitSpecification(int orgUnitId)
        : base(e => e.OrgUnitId == orgUnitId)
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}
