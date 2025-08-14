using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class LeaveRequestsByStatusSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsByStatusSpecification(LeaveRequestStatus status)
        : base(l => l.Status == status)
    {
        ApplyOrderByDescending(l => l.Period.Start);
        EnableNoTracking();
    }
}