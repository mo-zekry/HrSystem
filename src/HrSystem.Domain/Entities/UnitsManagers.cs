using HrSystem.Domain.Abstractions;

namespace HrSystem.Domain.Entities;

public class UnitsManagers : BaseEntity, IAggregateRoot
{
    public int OrgUnitId { get; set; }
    public OrgUnit OrgUnit { get; set; } = null!;
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;

    private UnitsManagers() { }

    public UnitsManagers(int orgUnitId, int employeeId)
    {
        OrgUnitId = orgUnitId;
        EmployeeId = employeeId;
    }
}
