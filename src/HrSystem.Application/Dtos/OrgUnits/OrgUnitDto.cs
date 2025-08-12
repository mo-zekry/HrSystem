namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitDto(
    int Id,
    string Name,
    int OrgTypeId,
    int? ParentId,
    int? ManagerId,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
