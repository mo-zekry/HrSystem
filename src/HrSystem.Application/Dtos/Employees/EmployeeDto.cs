using HrSystem.Domain.Enums;

namespace HrSystem.Application.Dtos.Employees;

public sealed record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string PositionArabic,
    string PositionEnglish,
    DateOnly HireDate,
    int OrgUnitId,
    EmployeeStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    IReadOnlyCollection<int> ManagedOrgUnitIds
);
