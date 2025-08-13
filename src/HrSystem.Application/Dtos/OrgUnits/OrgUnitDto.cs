namespace HrSystem.Application.Dtos.OrgUnits;

public sealed record OrgUnitDto(
    int Id,
    string Name,
    int OrgTypeId,
    int? ParentId,
    IReadOnlyCollection<int> ManagerIds,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
