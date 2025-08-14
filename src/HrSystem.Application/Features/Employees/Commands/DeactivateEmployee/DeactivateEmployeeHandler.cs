using HrSystem.Application.Repositories;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Features.Employees.Commands.DeactivateEmployee;

internal sealed class DeactivateEmployeeCommandHandler(IRepository<Domain.Entities.Employee> repository)
    : IRequestHandler<DeactivateEmployeeCommand>
{
    private readonly IRepository<Domain.Entities.Employee> _repository = repository;

    public async Task Handle(DeactivateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee {request.Id} not found");

        employee.Status = EmployeeStatus.Inactive;
        await _repository.UpdateAsync(employee, cancellationToken);
    }
}