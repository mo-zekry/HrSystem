using HrSystem.Domain.Enums;
using MediatR;

namespace HrSystem.Application.Features.Employees.Commands.UpdateEmployee;

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
