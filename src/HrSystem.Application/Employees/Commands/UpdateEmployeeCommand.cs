using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Employees.Commands;

public sealed record UpdateEmployeeCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    int? OrgUnitId
) : IRequest;

internal sealed class UpdateEmployeeCommandHandler(IRepository<Employee> repository)
    : IRequestHandler<UpdateEmployeeCommand>
{
    private readonly IRepository<Employee> _repository = repository;

    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee {request.Id} not found");

        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.Email = request.Email;
    if (request.OrgUnitId is int ouId)
            employee.OrgUnitId = ouId;

        await _repository.UpdateAsync(employee, cancellationToken);
    }
}
