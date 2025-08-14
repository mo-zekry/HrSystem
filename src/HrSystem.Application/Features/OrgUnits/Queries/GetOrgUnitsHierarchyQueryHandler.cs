using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Application.Features.OrgUnits.Queries.Flat;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Queries;

internal sealed class GetOrgUnitsHierarchyQueryHandler(IRepository<OrgUnit> repository)
    : IRequestHandler<GetOrgUnitsHierarchyQuery, IReadOnlyList<OrgUnitNodeDto>>
{
    private readonly IRepository<OrgUnit> _repository = repository;

    public async Task<IReadOnlyList<OrgUnitNodeDto>> Handle(
        GetOrgUnitsHierarchyQuery request,
        CancellationToken ct
    )
    {
        var rootId = request.RootId;

        // postgresql query to get org units with extra fields
        var rows = await _repository.FromSqlToListAsync<OrgUnitFlatWithManagers>(
            $@"
                WITH RECURSIVE cte AS (
                    SELECT
                        ou.""Id"" AS Id,
                        ou.""Name"" AS Name,
                        ou.""OrgTypeId"" AS OrgTypeId,
                        ot.""Name"" AS OrgTypeName,
                        ou.""ParentId"" AS ParentId,
                        0 AS Depth,
                        ARRAY[ou.""Id""]::int[] AS Path
                    FROM ""org_units"" ou
                    JOIN ""org_types"" ot ON ou.""OrgTypeId"" = ot.""Id""
                    WHERE
                        ({rootId} IS NULL AND ou.""ParentId"" IS NULL)
                        OR (ou.""Id"" = {rootId})

                    UNION ALL

                    SELECT
                        child.""Id"",
                        child.""Name"",
                        child.""OrgTypeId"",
                        ot2.""Name"" AS OrgTypeName,
                        child.""ParentId"",
                        cte.Depth + 1,
                        cte.Path || child.""Id""
                    FROM ""org_units"" child
                    JOIN cte ON child.""ParentId"" = cte.Id
                    JOIN ""org_types"" ot2 ON child.""OrgTypeId"" = ot2.""Id""
                )
                SELECT
                    cte.Id,
                    cte.Name,
                    cte.OrgTypeId,
                    cte.OrgTypeName,
                    cte.ParentId,
                    cte.Depth,
                    cte.Path,
                    ARRAY(
                        SELECT um.""EmployeeId"" FROM ""units_managers"" um WHERE um.""OrgUnitId"" = cte.Id
                    ) AS ManagerIds,
                    (
                        SELECT COUNT(*) FROM ""employees"" e WHERE e.""OrgUnitId"" = cte.Id
                    ) AS EmployeeCount
                FROM cte
                ORDER BY cte.Path
    ",
            ct
        );

        // Fetch manager details for all manager ids in one go
        var allManagerIds = rows.SelectMany(r => r.ManagerIds).Distinct().ToList();
        var managerEmployees =
            allManagerIds.Count > 0
                ? await _repository.FromSqlToListAsync<Employee>(
                    $"SELECT * FROM \"employees\" WHERE \"Id\" = ANY({allManagerIds.ToArray()})",
                    ct
                )
                : new List<Employee>();
        var managerLookup = managerEmployees.ToDictionary(e => e.Id);

        var byId = new Dictionary<int, OrgUnitNodeDto>(rows.Count);
        var roots = new List<OrgUnitNodeDto>();
        foreach (var r in rows)
        {
            var managers = r
                .ManagerIds.Select(mid =>
                    managerLookup.TryGetValue(mid, out var emp)
                        ? new OrgUnitManagerDto(emp.Id, emp.FirstName, emp.LastName, emp.Email)
                        : null
                )
                .Where(m => m != null)
                .ToList();

            var node = new OrgUnitNodeDto(
                r.Id,
                r.Name,
                r.OrgTypeId,
                r.OrgTypeName,
                r.ParentId,
                managers,
                new List<OrgUnitNodeDto>(),
                r.EmployeeCount
            );
            byId[r.Id] = node;
            if (r.ParentId is int pid && byId.TryGetValue(pid, out var p))
                ((List<OrgUnitNodeDto>)p.Children).Add(node);
            else
                roots.Add(node);
        }
        void Sort(OrgUnitNodeDto n)
        {
            var kids = (List<OrgUnitNodeDto>)n.Children;
            kids.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            foreach (var k in kids)
                Sort(k);
        }
        foreach (var r in roots)
            Sort(r);
        return roots;
    }
}
