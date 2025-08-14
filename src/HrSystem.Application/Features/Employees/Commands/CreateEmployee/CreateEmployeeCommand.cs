using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Features.Employee.Commands.CreateEmployee.Command;

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