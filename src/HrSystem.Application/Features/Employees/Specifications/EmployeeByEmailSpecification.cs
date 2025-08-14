using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class EmployeeByEmailSpecification : BaseSpecification<Employee>
{
    public EmployeeByEmailSpecification(string email)
        : base(e => e.Email == email)
    {
        EnableNoTracking();
    }
}