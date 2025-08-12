using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class OrgUnit : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public int OrgTypeId { get; set; }
    public int? ParentId { get; set; }
    public int? ManagerId { get; set; }

    private OrgUnit() { }

    public OrgUnit(string name, int orgTypeId, int? parentId = null, int? managerId = null)
    {
        Name = name;
        OrgTypeId = orgTypeId;
        ParentId = parentId;
        ManagerId = managerId;
    }
}
