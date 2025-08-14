using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Features.Employees.Specifications;
using HrSystem.Application.Repositories;
using MediatR;

namespace HrSystem.Application.Features.Employees.Queries;

internal sealed class GetEmployeesPagedQueryHandler(IRepository<Domain.Entities.Employee> repository)
    : IRequestHandler<GetEmployeesPagedQuery, PagedResultDto<EmployeeDto>>
{
    private readonly IRepository<Domain.Entities.Employee> _repository = repository;

    public async Task<PagedResultDto<EmployeeDto>> Handle(
        GetEmployeesPagedQuery request,
        CancellationToken cancellationToken
    )
    {
        // Build a simple spec inline to avoid over-proliferation of types
        var spec = new InlineSpecification(
            request.OrgUnitId,
            request.SearchTerm,
            request.Page,
            request.PageSize
        );
        var items = await _repository.ListAsync(spec, cancellationToken);
        // same filters for count but without paging
        var countSpec = new InlineSpecification(request.OrgUnitId, request.SearchTerm, null, null);
        var total = await _repository.CountAsync(countSpec, cancellationToken);
        return (items, total, request.Page, request.PageSize).ToPagedDto(e => e.ToDto());
    }
}