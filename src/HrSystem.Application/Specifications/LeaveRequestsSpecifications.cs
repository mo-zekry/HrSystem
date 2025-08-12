using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;
using HrSystem.Domain.ValueObjects;

namespace HrSystem.Application.Specifications.LeaveRequests;

// Get by Id
public sealed class LeaveRequestByIdSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestByIdSpecification(int id)
        : base(l => l.Id == id)
    {
        EnableNoTracking();
    }
}

// Requests for a given employee
public sealed class LeaveRequestsByEmployeeSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsByEmployeeSpecification(int employeeId)
        : base(l => l.EmployeeId == employeeId)
    {
        ApplyOrderByDescending(l => l.Period.Start);
        EnableNoTracking();
    }
}

// Requests overlapping a given period
public sealed class LeaveRequestsOverlappingPeriodSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsOverlappingPeriodSpecification(DateRange period)
        : base(l => l.Period.Start <= period.End && period.Start <= l.Period.End)
    {
        ApplyOrderBy(l => l.Period.Start);
        EnableNoTracking();
    }
}

// Requests by status
public sealed class LeaveRequestsByStatusSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsByStatusSpecification(LeaveRequestStatus status)
        : base(l => l.Status == status)
    {
        ApplyOrderByDescending(l => l.Period.Start);
        EnableNoTracking();
    }
}
