using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Employees.Commands;

public sealed record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string PositionArabic,
    string PositionEnglish,
    DateOnly HireDate,
    int OrgUnitId,
    EmployeeStatus Status,
    IReadOnlyCollection<int> ManagedOrgUnitIds
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
            request.PositionArabic,
            request.PositionEnglish,
            request.HireDate,
            request.OrgUnitId,
            request.Status
        );

        // Set managed units if provided
        if (request.ManagedOrgUnitIds != null && request.ManagedOrgUnitIds.Count > 0)
        {
            foreach (var unitId in request.ManagedOrgUnitIds)
            {
                employee.ManagedUnits.Add(new UnitsManagers(unitId, 0)); // EmployeeId will be set after save
            }
        }

        await _repository.AddAsync(employee, cancellationToken);
        return employee.Id;
    }
}
