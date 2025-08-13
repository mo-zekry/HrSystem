namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitNodeDto(
    int Id,
    string Name,
    int OrgTypeId,
    string OrgTypeName,
    int? ParentId,
    IReadOnlyCollection<OrgUnitManagerDto> Managers,
    IReadOnlyList<OrgUnitNodeDto> Children,
    int EmployeeCount
);

public sealed record OrgUnitManagerDto(
    int EmployeeId,
    string FirstName,
    string LastName,
    string Email
);
