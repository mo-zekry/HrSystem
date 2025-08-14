using HrSystem.Application.Dtos.OrgTypes;
using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Queries;

public sealed record GetOrgTypeByIdQuery(int Id) : IRequest<OrgTypeDto?>;
