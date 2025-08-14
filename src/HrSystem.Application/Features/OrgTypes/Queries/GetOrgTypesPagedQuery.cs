using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.OrgTypes;
using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Queries;

public sealed record GetOrgTypesPagedQuery(int Page, int PageSize, string? SearchTerm)
    : IRequest<PagedResultDto<OrgTypeDto>>;
    