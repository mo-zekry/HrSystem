using HrSystem.Domain.Abstractions;
using HrSystem.Domain.Enums;
using HrSystem.Domain.ValueObjects;

namespace HrSystem.Domain.Entities;

public class LeaveRequest : BaseEntity, IAggregateRoot
{
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public DateRange Period { get; set; } = null!;
    public string Reason { get; set; } = string.Empty;
    public LeaveRequestStatus Status { get; set; }

    private LeaveRequest() { }

    public LeaveRequest(int employeeId, DateRange period, string reason)
    {
        EmployeeId = employeeId;
        Period = period;
        Reason = reason;
        Status = LeaveRequestStatus.Pending;
    }

    public DateOnly StartDate => Period.Start;
    public DateOnly EndDate => Period.End;
}
