using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Features.Employees.Specifications;
using HrSystem.Application.Repositories;
using MediatR;

namespace HrSystem.Application.Features.Employees.Queries;

public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto?>;

internal sealed class GetEmployeeByIdQueryHandler(IRepository<Domain.Entities.Employee> repository)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
    private readonly IRepository<Domain.Entities.Employee> _repository = repository;

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