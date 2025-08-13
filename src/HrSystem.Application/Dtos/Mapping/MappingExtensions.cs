using HrSystem.Application.Dtos.Common;
using HrSystem.Application.Dtos.Employees;
using HrSystem.Application.Dtos.LeaveRequests;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Dtos.Mapping;

public static class MappingExtensions
{
    public static IReadOnlyList<OrgUnitDto> ToDto(this IEnumerable<OrgUnit> orgUnits) =>
        orgUnits.Select(o => o.ToDto()).ToList();

    public static EmployeeDto ToDto(this Employee e) =>
        new(
            e.Id,
            e.FirstName,
            e.LastName,
            e.Email,
            e.PositionArabic,
            e.PositionEnglish,
            e.HireDate,
            e.OrgUnitId,
            e.Status,
            e.CreatedDate,
            e.UpdatedDate,
            e.ManagedUnits.Select(mu => mu.OrgUnitId).ToList()
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
        new(
            o.Id,
            o.Name,
            o.OrgTypeId,
            o.ParentId,
            o.Managers.Select(m => m.EmployeeId).ToList(),
            o.CreatedDate,
            o.UpdatedDate
        );

    public static OrgUnitNodeDto ToNodeDto(
        this OrgUnit o,
        IReadOnlyList<OrgUnitNodeDto> children,
        string? orgTypeName = null,
        IReadOnlyCollection<OrgUnitManagerDto>? managers = null,
        int employeeCount = 0
    ) =>
        new(
            o.Id,
            o.Name,
            o.OrgTypeId,
            orgTypeName ?? o.OrgType?.Name ?? string.Empty,
            o.ParentId,
            managers ?? o.Managers.Select(m => new OrgUnitManagerDto(
                m.Employee.Id,
                m.Employee.FirstName,
                m.Employee.LastName,
                m.Employee.Email
            )).ToList(),
            children,
            employeeCount
        );

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
