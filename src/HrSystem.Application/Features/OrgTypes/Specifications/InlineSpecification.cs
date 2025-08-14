using HrSystem.Application.Specifications;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Features.OrgTypes.Specifications;

public sealed class InlineSpecification : BaseSpecification<OrgType>
{
    public InlineSpecification(string? searchTerm, int? page, int? pageSize)
    {
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            Where(t => t.Name.Contains(term));
        }

        ApplyOrderBy(t => t.Name);

        if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
        {
            var skip = (page.Value - 1) * pageSize.Value;
            ApplyPaging(skip, pageSize.Value);
        }

        EnableNoTracking();
    }
}