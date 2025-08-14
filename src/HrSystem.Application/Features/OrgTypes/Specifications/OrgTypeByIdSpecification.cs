using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgTypes.Specifications;

public sealed class OrgTypeByIdSpecification : BaseSpecification<OrgType>
{
    public OrgTypeByIdSpecification(int id)
        : base(t => t.Id == id)
    {
        EnableNoTracking();
    }
}