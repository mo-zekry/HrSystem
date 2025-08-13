using HrSystem.Application.Dtos.LeaveRequests;
using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.LeaveRequests.Queries;

// Assumption: "Manager" refers to OrgUnits where ManagerId == ManagerId; we return requests of employees in those org units
public sealed record GetLeaveRequestsForManagerQuery(int ManagerId)
    : IRequest<IReadOnlyList<LeaveRequestDto>>;

internal sealed class GetLeaveRequestsForManagerQueryHandler(
    IRepository<OrgUnit> orgUnitRepository,
    IRepository<Employee> employeeRepository,
    IRepository<LeaveRequest> leaveRepository
) : IRequestHandler<GetLeaveRequestsForManagerQuery, IReadOnlyList<LeaveRequestDto>>
{
    private readonly IRepository<OrgUnit> _orgUnits = orgUnitRepository;
    private readonly IRepository<Employee> _employees = employeeRepository;
    private readonly IRepository<LeaveRequest> _leaves = leaveRepository;

    public async Task<IReadOnlyList<LeaveRequestDto>> Handle(
        GetLeaveRequestsForManagerQuery request,
        CancellationToken cancellationToken
    )
    {
        // 1) All org units managed by this manager
        var managedSpec = new ManagedBySpec(request.ManagerId);
        var managed = await _orgUnits.ListAsync(managedSpec, cancellationToken);
        var managedIds = managed.Select(o => o.Id).ToHashSet();

        // 2) Employees in those org units
        var employeesSpec = new EmployeesInOrgUnitsSpec(managedIds);
        var employees = await _employees.ListAsync(employeesSpec, cancellationToken);
        var employeeIds = employees.Select(e => e.Id).ToHashSet();

        // 3) Leave requests for those employees
        var leavesSpec = new LeavesByEmployeesSpec(employeeIds);
        var leaves = await _leaves.ListAsync(leavesSpec, cancellationToken);
        return leaves.ToDto();
    }

    private sealed class ManagedBySpec : Application.Specifications.BaseSpecification<OrgUnit>
    {
        public ManagedBySpec(int managerId)
        {
            Where(o => o.Managers.Any(um => um.EmployeeId == managerId));
            EnableNoTracking();
        }
    }

    private sealed class EmployeesInOrgUnitsSpec : Specifications.BaseSpecification<Employee>
    {
        public EmployeesInOrgUnitsSpec(ISet<int> orgUnitIds)
        {
            Where(e => orgUnitIds.Contains(e.OrgUnitId));
            EnableNoTracking();
        }
    }

    private sealed class LeavesByEmployeesSpec
        : Application.Specifications.BaseSpecification<LeaveRequest>
    {
        public LeavesByEmployeesSpec(ISet<int> employeeIds)
        {
            Where(l => employeeIds.Contains(l.EmployeeId));
            ApplyOrderByDescending(l => l.Period.Start);
            EnableNoTracking();
        }
    }
}
