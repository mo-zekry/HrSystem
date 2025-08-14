using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class EmployeesInOrgUnitsSpecification : BaseSpecification<Employee>
{
    public EmployeesInOrgUnitsSpecification(ISet<int> orgUnitIds)
    {
        Where(e => orgUnitIds.Contains(e.OrgUnitId));
        EnableNoTracking();
    }
}