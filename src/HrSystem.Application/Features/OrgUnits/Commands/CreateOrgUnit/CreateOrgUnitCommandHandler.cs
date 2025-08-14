using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Commands.CreateOrgUnit;

internal sealed class CreateOrgUnitCommandHandler(IRepository<OrgUnit> repository)
    : IRequestHandler<CreateOrgUnitCommand, int>
{
    private readonly IRepository<OrgUnit> _repository = repository;

    public async Task<int> Handle(CreateOrgUnitCommand request, CancellationToken cancellationToken)
    {
        var orgUnit = new OrgUnit(request.Name, request.OrgTypeId, request.ParentId);

        // Add all provided managers to the OrgUnit's Managers collection
        if (request.ManagerIds != null)
        {
            foreach (var managerId in request.ManagerIds)
            {
                orgUnit.Managers.Add(new UnitsManagers(orgUnit.Id, managerId));
            }
        }

        await _repository.AddAsync(orgUnit, cancellationToken);
        return orgUnit.Id;
    }
}