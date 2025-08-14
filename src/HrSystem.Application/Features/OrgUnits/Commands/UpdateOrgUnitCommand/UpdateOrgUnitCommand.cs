using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Commands.UpdateOrgUnitCommand;

public sealed record UpdateOrgUnitCommand(
    int Id,
    string Name,
    int OrgTypeId,
    int? ParentId,
    IReadOnlyCollection<int> ManagerIds
) : IRequest;
