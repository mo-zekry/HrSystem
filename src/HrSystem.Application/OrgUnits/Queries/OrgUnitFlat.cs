namespace HrSystem.Application.OrgUnits.Queries;

// Keyless projection row for OrgUnit hierarchy queries
public sealed class OrgUnitFlat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OrgTypeId { get; set; }
    public int? ParentId { get; set; }
    public int? ManagerId { get; set; }
    public int Depth { get; set; }
    public int[] Path { get; set; } = Array.Empty<int>();
}
