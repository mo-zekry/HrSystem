using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgTypes.Queries;

public sealed record GetOrgTypesPagedQuery(int Page, int PageSize, string? SearchTerm)
    : IRequest<PagedResultDto<OrgTypeDto>>;

internal sealed class GetOrgTypesPagedQueryHandler(IRepository<OrgType> repository)
    : IRequestHandler<GetOrgTypesPagedQuery, PagedResultDto<OrgTypeDto>>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task<PagedResultDto<OrgTypeDto>> Handle(
        GetOrgTypesPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        var spec = new InlineSpec(request.SearchTerm, request.Page, request.PageSize);
        var items = await _repository.ListAsync(spec, cancellationToken);
        var countSpec = new InlineSpec(request.SearchTerm, null, null);
        var total = await _repository.CountAsync(countSpec, cancellationToken);
        return (items, total, request.Page, request.PageSize).ToPagedDto(t => t.ToDto());
    }

    private sealed class InlineSpec : Application.Specifications.BaseSpecification<OrgType>
    {
        public InlineSpec(string? searchTerm, int? page, int? pageSize)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                Where(t => t.Name.Contains(term));
            }

            ApplyOrderBy(t => t.Name);

            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                var skip = (page.Value - 1) * pageSize.Value;
                ApplyPaging(skip, pageSize.Value);
            }

            EnableNoTracking();
        }
    }
}
