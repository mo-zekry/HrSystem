using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.Employees.Commands.CreateEmployee;

public sealed class CreateEmployeeCommandHandler(IRepository<Domain.Entities.Employee> repository)
    : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IRepository<Domain.Entities.Employee> _repository = repository;

    public async Task<int> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken
    )
    {
        var employee = new Domain.Entities.Employee(
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