using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class OrgType : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;

    // Navigation property for OrgUnits (one-to-many)
    public ICollection<OrgUnit> OrgUnits { get; set; } = new List<OrgUnit>();

    private OrgType() { }

    public OrgType(string name)
    {
        Name = name;
    }

    public override string ToString() => Name;
}
