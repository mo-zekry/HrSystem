using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.LeaveRequests.Specifications;

public sealed class LeavesByEmployeesSpecification
    : Application.Specifications.BaseSpecification<LeaveRequest>
{
    public LeavesByEmployeesSpecification(ISet<int> employeeIds)
    {
        Where(l => employeeIds.Contains(l.EmployeeId));
        ApplyOrderByDescending(l => l.Period.Start);
        EnableNoTracking();
    }
}