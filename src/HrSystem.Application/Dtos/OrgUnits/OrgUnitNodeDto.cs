namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitNodeDto(
    Guid Id,
    string Name,
    Guid OrgTypeId,
    Guid? ParentId,
    Guid? ManagerId,
    IReadOnlyList<OrgUnitNodeDto> Children
);
