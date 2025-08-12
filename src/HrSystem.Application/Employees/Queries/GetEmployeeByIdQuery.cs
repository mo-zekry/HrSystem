using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Repositories;
using HrSystem.Application.Specifications.Employees;
using MediatR;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Employees.Queries;

public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>;

internal sealed class GetEmployeeByIdQueryHandler(IRepository<Employee> repository)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task<EmployeeDto?> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        // Use specification to benefit from AsNoTracking
        var spec = new EmployeeByIdSpecification(request.Id);
        var list = await _repository.ListAsync(spec, cancellationToken);
        return list.FirstOrDefault()?.ToDto();
    }
}
