using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgUnits.Specifications;

public sealed class OrgUnitsManagedBySpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitsManagedBySpecification(int managerId)
        : base(o => o.Managers.Any(um => um.EmployeeId == managerId))
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}
