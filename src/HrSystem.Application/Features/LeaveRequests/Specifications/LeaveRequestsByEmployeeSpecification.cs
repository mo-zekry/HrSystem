using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class LeaveRequestsByEmployeeSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsByEmployeeSpecification(int employeeId)
        : base(l => l.EmployeeId == employeeId)
    {
        ApplyOrderByDescending(l => l.Period.Start);
        EnableNoTracking();
    }
}