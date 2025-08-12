namespace HrSystem.Application.OrgUnits.Queries;

// Keyless projection row for OrgUnit hierarchy queries
public sealed class OrgUnitFlat
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid OrgTypeId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? ManagerId { get; set; }
    public int Depth { get; set; }
    public Guid[] Path { get; set; } = Array.Empty<Guid>();
}
