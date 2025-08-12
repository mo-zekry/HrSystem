using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Employees.Queries;

public sealed record GetEmployeesPagedQuery(
    int Page,
    int PageSize,
    int? OrgUnitId,
    string? SearchTerm
) : IRequest<PagedResultDto<EmployeeDto>>;

internal sealed class GetEmployeesPagedQueryHandler(IRepository<Employee> repository)
    : IRequestHandler<GetEmployeesPagedQuery, PagedResultDto<EmployeeDto>>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task<PagedResultDto<EmployeeDto>> Handle(
        GetEmployeesPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        // Build a simple spec inline to avoid over-proliferation of types
        var spec = new InlineSpec(
            request.OrgUnitId,
            request.SearchTerm,
            request.Page,
            request.PageSize
        );
        var items = await _repository.ListAsync(spec, cancellationToken);
        // same filters for count but without paging
        var countSpec = new InlineSpec(request.OrgUnitId, request.SearchTerm, null, null);
    var total = await _repository.CountAsync(countSpec, cancellationToken);
    return (items, total, request.Page, request.PageSize).ToPagedDto(e => e.ToDto());
    }

    private sealed class InlineSpec : Application.Specifications.BaseSpecification<Employee>
    {
        public InlineSpec(int? orgUnitId, string? searchTerm, int? page, int? pageSize)
        {
            if (orgUnitId is int ou && !string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                Where(e =>
                    e.OrgUnitId == ou
                    && ((e.FirstName + " " + e.LastName).Contains(term) || e.Email.Contains(term))
                );
            }
            else if (orgUnitId is int onlyOu)
            {
                Where(e => e.OrgUnitId == onlyOu);
            }
            else if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                Where(e =>
                    (e.FirstName + " " + e.LastName).Contains(term) || e.Email.Contains(term)
                );
            }

            ApplyOrderBy(e => e.LastName);

            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                var skip = (page.Value - 1) * pageSize.Value;
                ApplyPaging(skip, pageSize.Value);
            }

            EnableNoTracking();
        }
    }
}
