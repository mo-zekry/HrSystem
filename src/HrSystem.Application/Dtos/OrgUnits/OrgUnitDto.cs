namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitDto(
    Guid Id,
    string Name,
    Guid OrgTypeId,
    Guid? ParentId,
    Guid? ManagerId,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
