using HrSystem.Domain.Entities;

namespace HrSystem.Application.Specifications.OrgTypes;

// By Id
public sealed class OrgTypeByIdSpecification : BaseSpecification<OrgType>
{
    public OrgTypeByIdSpecification(int id)
        : base(t => t.Id == id)
    {
        EnableNoTracking();
    }
}

// By name exact
public sealed class OrgTypeByNameSpecification : BaseSpecification<OrgType>
{
    public OrgTypeByNameSpecification(string name)
        : base(t => t.Name == name)
    {
        EnableNoTracking();
    }
}

// Name contains term
public sealed class OrgTypesSearchByNameSpecification : BaseSpecification<OrgType>
{
    public OrgTypesSearchByNameSpecification(string term)
        : base(t => t.Name.Contains(term))
    {
        ApplyOrderBy(t => t.Name);
        EnableNoTracking();
    }
}
