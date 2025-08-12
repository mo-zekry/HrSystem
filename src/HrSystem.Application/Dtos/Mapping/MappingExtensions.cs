using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.LeaveRequests;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Dtos.Mapping;

public static class MappingExtensions
{
    public static EmployeeDto ToDto(this Employee e) =>
        new(
            e.Id,
            e.FirstName,
            e.LastName,
            e.Email,
            e.HireDate,
            e.OrgUnitId,
            e.Status,
            e.CreatedDate,
            e.UpdatedDate
        );

    public static IReadOnlyList<EmployeeDto> ToDto(this IEnumerable<Employee> employees) =>
        employees.Select(e => e.ToDto()).ToList();

    public static LeaveRequestDto ToDto(this LeaveRequest l) =>
        new(
            l.Id,
            l.EmployeeId,
            l.StartDate,
            l.EndDate,
            l.Reason,
            l.Status,
            l.CreatedDate,
            l.UpdatedDate
        );

    public static IReadOnlyList<LeaveRequestDto> ToDto(this IEnumerable<LeaveRequest> leaves) =>
        leaves.Select(l => l.ToDto()).ToList();

    public static OrgUnitDto ToDto(this OrgUnit o) =>
        new(o.Id, o.Name, o.OrgTypeId, o.ParentId, o.ManagerId, o.CreatedDate, o.UpdatedDate);

    public static OrgUnitNodeDto ToNodeDto(
        this OrgUnit o,
        IReadOnlyList<OrgUnitNodeDto> children
    ) => new(o.Id, o.Name, o.OrgTypeId, o.ParentId, o.ManagerId, children);

    public static OrgTypeDto ToDto(this OrgType t) =>
        new(t.Id, t.Name, t.CreatedDate, t.UpdatedDate);

    public static PagedResultDto<TDto> ToPagedDto<T, TDto>(
        this (IReadOnlyList<T> Items, int TotalCount, int Page, int PageSize) result,
        System.Func<T, TDto> map
    )
    {
        return new PagedResultDto<TDto>(
            result.Items.Select(map).ToList(),
            result.TotalCount,
            result.Page,
            result.PageSize
        );
    }
}
