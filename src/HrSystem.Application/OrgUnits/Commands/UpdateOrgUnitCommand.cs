using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgUnits.Commands;

public sealed record UpdateOrgUnitCommand(
    int Id,
    string Name,
    int OrgTypeId,
    int? ParentId,
    int? ManagerId
) : IRequest;

internal sealed class UpdateOrgUnitCommandHandler(IRepository<OrgUnit> repository)
    : IRequestHandler<UpdateOrgUnitCommand>
{
    private readonly IRepository<OrgUnit> _repository = repository;

    public async Task Handle(UpdateOrgUnitCommand request, CancellationToken cancellationToken)
    {
        var orgUnit =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"OrgUnit {request.Id} not found");

    orgUnit.Name = request.Name;
    // OrgTypeId can be changed if needed; keeping as-is unless business rules say otherwise
    orgUnit.ParentId = request.ParentId;
    orgUnit.ManagerId = request.ManagerId;

        await _repository.UpdateAsync(orgUnit, cancellationToken);
    }
}
