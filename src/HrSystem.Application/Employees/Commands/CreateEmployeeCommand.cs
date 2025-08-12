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
    int OrgUnitId
) : IRequest<int>;

internal sealed class CreateEmployeeCommandHandler(IRepository<Employee> repository)
    : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task<int> Handle(
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
