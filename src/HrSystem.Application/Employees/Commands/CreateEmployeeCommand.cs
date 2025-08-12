using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Employees.Commands;

public sealed record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    DateOnly HireDate,
    Guid OrgUnitId
) : IRequest<Guid>;

internal sealed class CreateEmployeeCommandHandler(IRepository<Employee> repository)
    : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task<Guid> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken
    )
    {
        var employee = new Employee(
            request.FirstName,
            request.LastName,
            request.Email,
            request.HireDate,
            request.OrgUnitId,
            EmployeeStatus.Active
        );

        await _repository.AddAsync(employee, cancellationToken);
        return employee.Id;
    }
}
