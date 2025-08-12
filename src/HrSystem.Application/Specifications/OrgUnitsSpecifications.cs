using HrSystem.Domain.Entities;

namespace HrSystem.Application.Specifications.OrgUnits;

// By Id
public sealed class OrgUnitByIdSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitByIdSpecification(int id)
        : base(o => o.Id == id)
    {
        EnableNoTracking();
    }
}

// Children of a parent org unit
public sealed class OrgUnitChildrenSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitChildrenSpecification(int parentId)
        : base(o => o.ParentId == parentId)
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}

// By org type
public sealed class OrgUnitsByTypeSpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitsByTypeSpecification(int orgTypeId)
        : base(o => o.OrgTypeId == orgTypeId)
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}

// With optional manager
public sealed class OrgUnitsManagedBySpecification : BaseSpecification<OrgUnit>
{
    public OrgUnitsManagedBySpecification(int managerId)
        : base(o => o.ManagerId == managerId)
    {
        ApplyOrderBy(o => o.Name);
        EnableNoTracking();
    }
}
