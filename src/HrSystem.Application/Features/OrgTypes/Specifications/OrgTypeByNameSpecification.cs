using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgTypes.Specifications;

public sealed class OrgTypeByNameSpecification : BaseSpecification<OrgType>
{
    public OrgTypeByNameSpecification(string name)
        : base(t => t.Name == name)
    {
        EnableNoTracking();
    }
}
