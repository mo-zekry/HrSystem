using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class OrgUnit : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public int OrgTypeId { get; set; }
    public OrgType OrgType { get; set; } = null!;
    public int? ParentId { get; set; }
    public OrgUnit? Parent { get; set; }
    public ICollection<OrgUnit> Children { get; set; } = new List<OrgUnit>();

    // Navigation property for managers (many-to-many)
    public ICollection<UnitsManagers> Managers { get; set; } = new List<UnitsManagers>();

    private OrgUnit() { }

    public OrgUnit(
        string name,
        int orgTypeId,
        int? parentId = null,
        ICollection<UnitsManagers>? managers = null
    )
    {
        Name = name;
        OrgTypeId = orgTypeId;
        ParentId = parentId;
        Managers = managers ?? new List<UnitsManagers>();
    }
}
