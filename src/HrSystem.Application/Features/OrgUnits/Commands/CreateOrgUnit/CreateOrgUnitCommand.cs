using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Commands.CreateOrgUnit;


public sealed record CreateOrgUnitCommand(
    string Name,
    int OrgTypeId,
    int? ParentId,
    IReadOnlyCollection<int> ManagerIds
) : IRequest<int>;
