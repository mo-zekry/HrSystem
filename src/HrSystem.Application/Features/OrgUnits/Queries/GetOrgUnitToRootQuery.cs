using HrSystem.Application.Dtos.OrgUnits;
using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Queries;

public sealed record GetOrgUnitToRootQuery(int OrgUnitId) : IRequest<IReadOnlyList<OrgUnitDto>>;
