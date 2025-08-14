using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.Features.OrgTypes.Specifications;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Queries;

internal sealed class GetOrgTypesPagedQueryHandler(IRepository<OrgType> repository)
    : IRequestHandler<GetOrgTypesPagedQuery, PagedResultDto<OrgTypeDto>>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task<PagedResultDto<OrgTypeDto>> Handle(
        GetOrgTypesPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        var spec = new InlineSpecification(request.SearchTerm, request.Page, request.PageSize);
        var items = await _repository.ListAsync(spec, cancellationToken);
        var countSpec = new InlineSpecification(request.SearchTerm, null, null);
        var total = await _repository.CountAsync(countSpec, cancellationToken);
        return (items, total, request.Page, request.PageSize).ToPagedDto(t => t.ToDto());
    }
}