using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class OrgUnit : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public Guid OrgTypeId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? ManagerId { get; set; }

    private OrgUnit() { }

    public OrgUnit(string name, Guid orgTypeId, Guid? parentId = null, Guid? managerId = null)
    {
        Name = name;
        OrgTypeId = orgTypeId;
        ParentId = parentId;
        ManagerId = managerId;
    }
}
