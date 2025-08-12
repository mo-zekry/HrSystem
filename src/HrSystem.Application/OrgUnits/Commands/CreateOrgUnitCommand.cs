using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgUnits.Commands;

public sealed record CreateOrgUnitCommand(
    string Name,
    int OrgTypeId,
    int? ParentId,
    int? ManagerId
) : IRequest<int>;

internal sealed class CreateOrgUnitCommandHandler(IRepository<OrgUnit> repository)
    : IRequestHandler<CreateOrgUnitCommand, int>
{
    private readonly IRepository<OrgUnit> _repository = repository;

    public async Task<int> Handle(
        CreateOrgUnitCommand request,
        CancellationToken cancellationToken
    )
    {
        var orgUnit = new OrgUnit(
            request.Name,
            request.OrgTypeId,
            request.ParentId,
            request.ManagerId
        );
        await _repository.AddAsync(orgUnit, cancellationToken);
        return orgUnit.Id;
    }
}
