using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Dtos.OrgUnits;
using HrSystem.Application.Repositories;
using HrSystem.Application.Specifications.OrgUnits;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgUnits.Queries;

public sealed record GetOrgUnitToRootQuery(int OrgUnitId) : IRequest<IReadOnlyList<OrgUnitDto>>;

public sealed class GetOrgUnitToRootQueryHandler
    : IRequestHandler<GetOrgUnitToRootQuery, IReadOnlyList<OrgUnitDto>>
{
    private readonly IRepository<OrgUnit> _orgUnitRepo;

    public GetOrgUnitToRootQueryHandler(IRepository<OrgUnit> orgUnitRepository)
    {
        _orgUnitRepo = orgUnitRepository;
    }

    public async Task<IReadOnlyList<OrgUnitDto>> Handle(
        GetOrgUnitToRootQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = new List<OrgUnit>();
        var currentId = request.OrgUnitId;
        while (true)
        {
            var orgUnits = await _orgUnitRepo.ListAsync(
                new OrgUnitByIdSpecification(currentId),
                cancellationToken
            );
            var orgUnit = orgUnits.FirstOrDefault();
            if (orgUnit == null)
                break;
            result.Add(orgUnit);
            if (orgUnit.ParentId == null)
                break;
            currentId = orgUnit.ParentId.Value;
        }
        return result.ToDto();
    }
}
