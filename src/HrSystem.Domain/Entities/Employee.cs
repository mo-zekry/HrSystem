using HrSystem.Domain.Abstractions;
using HrSystem.Domain.Enums;

namespace HrSystem.Domain.Entities;

public class Employee : BaseEntity, IAggregateRoot
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateOnly HireDate { get; set; }
    public Guid OrgUnitId { get; set; }
    public EmployeeStatus Status { get; set; }

    private Employee() { }

    public Employee(
        string firstName,
        string lastName,
        string email,
        DateOnly hireDate,
        Guid orgUnitId,
        EmployeeStatus status = EmployeeStatus.Active
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HireDate = hireDate;
        OrgUnitId = orgUnitId;
        Status = status;
    }
}
