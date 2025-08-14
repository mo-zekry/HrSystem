using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgUnits.Specifications;

public sealed class OrgUnitByIdSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitByIdSpecification(int id)
        : base(o => o.Id == id)
    {
        EnableNoTracking();
    }
}