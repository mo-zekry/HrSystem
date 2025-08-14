using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgTypes.Specifications;

public sealed class OrgTypesSearchByNameSpecification : BaseSpecification<OrgType>
{
    public OrgTypesSearchByNameSpecification(string term)
        : base(t => t.Name.Contains(term))
    {
        ApplyOrderBy(t => t.Name);
        EnableNoTracking();
    }
}