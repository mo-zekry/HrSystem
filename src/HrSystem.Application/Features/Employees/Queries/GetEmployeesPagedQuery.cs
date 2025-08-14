using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;

using MediatR;

namespace HrSystem.Application.Features.Employees.Queries;

public sealed record GetEmployeesPagedQuery(
    int Page,
    int PageSize,
    int? OrgUnitId,
    string? SearchTerm
) : IRequest<PagedResultDto<EmployeeDto>>;