using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Employees.Commands;

public sealed record DeactivateEmployeeCommand(Guid Id) : IRequest;

internal sealed class DeactivateEmployeeCommandHandler(IRepository<Employee> repository)
    : IRequestHandler<DeactivateEmployeeCommand>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee {request.Id} not found");

        employee.Status = EmployeeStatus.Inactive;
        await _repository.UpdateAsync(employee, cancellationToken);
    }
}
