using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class OrgType : BaseEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;

    private OrgType() { }

    public OrgType(string name)
    {
        Name = name;
    }
}
