using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Commands.UpdateOrgType;

public sealed record UpdateOrgTypeCommand(int Id, string Name) : IRequest;

