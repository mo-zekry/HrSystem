using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Employees.Commands;

public sealed record UpdateEmployeeCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PositionArabic,
    string PositionEnglish,
    int? OrgUnitId,
    EmployeeStatus Status,
    IReadOnlyCollection<int> ManagedOrgUnitIds
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
        employee.PositionArabic = request.PositionArabic;
        employee.PositionEnglish = request.PositionEnglish;
        if (request.OrgUnitId is int ouId)
            employee.OrgUnitId = ouId;
        employee.Status = request.Status;

        // Update managed units
        if (request.ManagedOrgUnitIds != null)
        {
            employee.ManagedUnits.Clear();
            foreach (var unitId in request.ManagedOrgUnitIds)
            {
                employee.ManagedUnits.Add(new UnitsManagers(unitId, employee.Id));
            }
        }

        await _repository.UpdateAsync(employee, cancellationToken);
    }
}
