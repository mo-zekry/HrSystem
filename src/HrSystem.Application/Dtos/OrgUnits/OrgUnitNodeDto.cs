namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitNodeDto(
    int Id,
    string Name,
    int OrgTypeId,
    int? ParentId,
    int? ManagerId,
    IReadOnlyList<OrgUnitNodeDto> Children
);
