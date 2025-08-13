using HrSystem.Domain.Abstractions;
using HrSystem.Domain.Enums;

namespace HrSystem.Domain.Entities;

public class Employee : BaseEntity, IAggregateRoot
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PositionArabic { get; set; } = null!;
    public string PositionEnglish { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly HireDate { get; set; }
    public int OrgUnitId { get; set; }
    public EmployeeStatus Status { get; set; }

    // Navigation property for managed units (many-to-many)
    public ICollection<UnitsManagers> ManagedUnits { get; set; } = new List<UnitsManagers>();

    // Convenience property for full name
    public string FullName => $"{FirstName} {LastName}";

    // Navigation property for OrgUnit (many-to-one)
    public OrgUnit OrgUnit { get; set; } = null!;

    private Employee() { }

    public Employee(
        string firstName,
        string lastName,
        string email,
        string positionArabic,
        string positionEnglish,
        DateOnly hireDate,
        int orgUnitId,
        EmployeeStatus status = EmployeeStatus.Active
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PositionArabic = positionArabic;
        PositionEnglish = positionEnglish;
        HireDate = hireDate;
        OrgUnitId = orgUnitId;
        Status = status;
    }
}
