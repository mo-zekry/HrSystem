using HrSystem.Application.Specifications;

namespace HrSystem.Application.Features.Employees.Specifications;

public sealed class InlineSpecification : BaseSpecification<Domain.Entities.Employee>
{
    public InlineSpecification(int? orgUnitId, string? searchTerm, int? page, int? pageSize)
    {
        if (orgUnitId is int ou && !string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            Where(e =>
                e.OrgUnitId == ou
                && ((e.FirstName + " " + e.LastName).Contains(term) || e.Email.Contains(term))
            );
        }
        else if (orgUnitId is int onlyOu)
        {
            Where(e => e.OrgUnitId == onlyOu);
        }
        else if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            Where(e =>
                (e.FirstName + " " + e.LastName).Contains(term) || e.Email.Contains(term)
            );
        }

        ApplyOrderBy(e => e.LastName);

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            var skip = (page.Value - 1) * pageSize.Value;
            ApplyPaging(skip, pageSize.Value);
        }

        EnableNoTracking();
    }
}