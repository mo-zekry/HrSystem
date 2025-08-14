using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgUnits.Specifications;

public sealed class OrgUnitsByTypeSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitsByTypeSpecification(int orgTypeId)
        : base(o => o.OrgTypeId == orgTypeId)
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}