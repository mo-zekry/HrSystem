namespace HrSystem.Application.Dtos.OrgTypes;

public sealed record OrgTypeDto(Guid Id, string Name, DateTime CreatedDate, DateTime? UpdatedDate);
