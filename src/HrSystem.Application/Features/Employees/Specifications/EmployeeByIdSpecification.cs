using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class EmployeeByIdSpecification : BaseSpecification<Employee>
{
    public EmployeeByIdSpecification(int id)
        : base(e => e.Id == id)
    {
        EnableNoTracking();
    }
}