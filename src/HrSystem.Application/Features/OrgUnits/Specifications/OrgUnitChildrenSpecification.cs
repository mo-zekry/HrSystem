using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgUnits.Specifications;

public sealed class OrgUnitChildrenSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitChildrenSpecification(int parentId)
        : base(o => o.ParentId == parentId)
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}