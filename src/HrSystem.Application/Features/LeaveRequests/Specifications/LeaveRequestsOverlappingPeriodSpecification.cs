using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;
using HrSystem.Domain.ValueObjects;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class LeaveRequestsOverlappingPeriodSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestsOverlappingPeriodSpecification(DateRange period)
        : base(l => l.Period.Start <= period.End && period.Start <= l.Period.End)
    {
        ApplyOrderBy(l => l.Period.Start);
        EnableNoTracking();
    }
}