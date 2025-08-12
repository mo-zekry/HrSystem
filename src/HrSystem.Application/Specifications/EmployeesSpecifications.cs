using HrSystem.Domain.Entities;
using HrSystem.Domain.Enums;

namespace HrSystem.Application.Specifications.Employees;

// Get by Id
public sealed class EmployeeByIdSpecification : BaseSpecification<Employee>
{
    public EmployeeByIdSpecification(int id)
        : base(e => e.Id == id)
    {
        EnableNoTracking();
    }
}

// Get by Email
public sealed class EmployeeByEmailSpecification : BaseSpecification<Employee>
{
    public EmployeeByEmailSpecification(string email)
        : base(e => e.Email == email)
    {
        EnableNoTracking();
    }
}

// Employees in an OrgUnit
public sealed class EmployeesByOrgUnitSpecification : BaseSpecification<Employee>
{
    public EmployeesByOrgUnitSpecification(int orgUnitId)
        : base(e => e.OrgUnitId == orgUnitId)
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}

// Active employees
public sealed class ActiveEmployeesSpecification : BaseSpecification<Employee>
{
    public ActiveEmployeesSpecification()
        : base(e => e.Status == EmployeeStatus.Active)
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}

// Employees hired between two dates (inclusive)
public sealed class EmployeesHiredInRangeSpecification : BaseSpecification<Employee>
{
    public EmployeesHiredInRangeSpecification(DateOnly start, DateOnly end)
        : base(e => e.HireDate >= start && e.HireDate <= end)
    {
        ApplyOrderBy(e => e.HireDate);
        EnableNoTracking();
    }
}

// Case-insensitive name search (Contains). Note: translation depends on provider; may evaluate client-side.
public sealed class EmployeesSearchByNameSpecification : BaseSpecification<Employee>
{
    public EmployeesSearchByNameSpecification(string term)
        : base(e => (e.FirstName + " " + e.LastName).Contains(term))
    {
        ApplyOrderBy(e => e.LastName);
        EnableNoTracking();
    }
}
