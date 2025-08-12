using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgUnits.Queries;

public sealed record GetOrgUnitsHierarchyQuery(Guid? RootId)
    : IRequest<IReadOnlyList<OrgUnitNodeDto>>;

internal sealed class GetOrgUnitsHierarchyQueryHandler(IRepository<OrgUnit> repository)
    : IRequestHandler<GetOrgUnitsHierarchyQuery, IReadOnlyList<OrgUnitNodeDto>>
{
    private readonly IRepository<OrgUnit> _repository = repository;

    // Example of a CTE-based implementation for Postgres SQL

    /*
        public async Task<IReadOnlyList<OrgUnitNodeDto>> Handle(
            GetOrgUnitsHierarchyQuery request,
            CancellationToken cancellationToken
        )
        {
            // Fetch all org units once; hierarchy building is in-memory
            var all = await _repository.ListAsync(new AllSpec(), cancellationToken);

            IEnumerable<OrgUnit> roots = request.RootId is Guid rootId
                ? all.Where(o => o.Id == rootId)
                : all.Where(o => o.ParentId == null);

            var lookup = all.ToLookup(o => o.ParentId);

            List<OrgUnitNodeDto> Build(Guid? parentId)
            {
                return lookup[parentId]
                    .OrderBy(o => o.Name)
                    .Select(o => new OrgUnitNodeDto(
                        o.Id,
                        o.Name,
                        o.OrgTypeId,
                        o.ParentId,
                        o.ManagerId,
                        Build(o.Id)
                    ))
                    .ToList();
            }

            if (request.RootId is Guid)
            {
                return Build(request.RootId);
            }

            return Build(null);
        }

        private sealed class AllSpec : Specifications.BaseSpecification<OrgUnit>
        {
            public AllSpec()
            {
                ApplyOrderBy(o => o.Name);
                EnableNoTracking();
            }
        }
    */

    // ===================================================

    // Example of a CTE-based implementation for Postgress Sql
    public async Task<IReadOnlyList<OrgUnitNodeDto>> Handle(
        GetOrgUnitsHierarchyQuery request,
        CancellationToken ct
    )
    {
        var rootId = request.RootId;

    var rows = await _repository.FromSqlToListAsync<OrgUnitFlat>(
        $@"
        WITH RECURSIVE cte AS (
            SELECT
                ou.""Id"" AS Id, ou.""Name"" AS Name, ou.""OrgTypeId"" AS OrgTypeId,
                ou.""ParentId"" AS ParentId, ou.""ManagerId"" AS ManagerId,
                0 AS Depth,
                ARRAY[ou.""Id""]::uuid[] AS Path
        FROM ""org_units"" ou
            WHERE
                ({rootId} IS NULL AND ou.""ParentId"" IS NULL)
                OR (ou.""Id"" = {rootId})

            UNION ALL

            SELECT
                child.""Id"", child.""Name"", child.""OrgTypeId"", child.""ParentId"", child.""ManagerId"",
                cte.Depth + 1,
                cte.Path || child.""Id""
        FROM ""org_units"" child
            JOIN cte ON child.""ParentId"" = cte.Id
        )
        SELECT Id, Name, OrgTypeId, ParentId, ManagerId, Depth, Path
        FROM cte
        ORDER BY Path
    ", ct);

        var byId = new Dictionary<Guid, OrgUnitNodeDto>(rows.Count);
        var roots = new List<OrgUnitNodeDto>();
        foreach (var r in rows)
        {
            var node = new OrgUnitNodeDto(
                r.Id,
                r.Name,
                r.OrgTypeId,
                r.ParentId,
                r.ManagerId,
                new List<OrgUnitNodeDto>()
            );
            byId[r.Id] = node;
            if (r.ParentId is Guid pid && byId.TryGetValue(pid, out var p))
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

    // Using top-level OrgUnitFlat type (registered as keyless in Infrastructure)

    // ===================================================

    /*

    // breadth-first search implementation

    public async Task<IReadOnlyList<OrgUnitNodeDto>> Handle(
        GetOrgUnitsHierarchyQuery request, CancellationToken ct)
    {
        var rootIds = request.RootId is Guid rid
            ? new List<Guid?> { rid }
            : new List<Guid?> { null };

        var childrenMap = new Dictionary<Guid?, List<OrgUnit>>();
        var allFetched = new Dictionary<Guid, OrgUnit>();
        var frontier = new List<Guid?>(rootIds);

        while (frontier.Count > 0)
        {
            var spec = new ByParentsSpec(frontier);
            var batch = await _repository.ListAsync(spec, ct);

            foreach (var o in batch)
            {
                allFetched[o.Id] = o;
                if (!childrenMap.TryGetValue(o.ParentId, out var list))
                    childrenMap[o.ParentId] = list = new List<OrgUnit>();
                list.Add(o);
            }

            frontier = batch.Select(b => (Guid?)b.Id).ToList();
        }

        List<OrgUnitNodeDto> Build(Guid? parentId)
            => (childrenMap.TryGetValue(parentId, out var kids) ? kids : Enumerable.Empty<OrgUnit>())
               .OrderBy(x => x.Name)
               .Select(o => new OrgUnitNodeDto(o.Id, o.Name, o.OrgTypeId, o.ParentId, o.ManagerId, Build(o.Id)))
               .ToList();

        return Build(request.RootId);
    }

    sealed class ByParentsSpec : Specifications.BaseSpecification<OrgUnit>
    {
        public ByParentsSpec(IEnumerable<Guid?> parents)
        {
            Criteria = o => parents.Contains(o.ParentId);
            EnableNoTracking();
        }
    }
    */
}
