using HrSystem.Application.Dtos.OrgUnits;

namespace HrSystem.Application.Features.OrgUnits.Queries.Flat;

// Keyless projection row for OrgUnit hierarchy queries
public sealed class OrgUnitFlatWithManagers
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int OrgTypeId { get; set; }
    public string OrgTypeName { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public int[] ManagerIds { get; set; } = Array.Empty<int>();
    public List<OrgUnitManagerDto> Managers { get; set; } = new();
    public int EmployeeCount { get; set; }
    public int Depth { get; set; }
    public int[] Path { get; set; } = Array.Empty<int>();
}