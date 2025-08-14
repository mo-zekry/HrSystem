using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Commands.CreateOrgType;

public sealed record CreateOrgTypeCommand(string Name) : IRequest<int>;
