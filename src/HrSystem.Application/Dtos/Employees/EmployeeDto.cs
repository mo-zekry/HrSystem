using HrSystem.Domain.Enums;

namespace HrSystem.Application.Dtos.Employees;

public sealed record EmployeeDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateOnly HireDate,
    Guid OrgUnitId,
    EmployeeStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate
);
