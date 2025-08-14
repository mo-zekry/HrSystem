using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class LeaveRequestByIdSpecification : BaseSpecification<LeaveRequest>
{
    public LeaveRequestByIdSpecification(int id)
        : base(l => l.Id == id)
    {
        EnableNoTracking();
    }
}