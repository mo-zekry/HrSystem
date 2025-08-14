using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgUnits.Commands.UpdateOrgUnitCommand;

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

        // Update managers (many-to-many)
        if (request.ManagerIds != null)
        {
            // Remove managers not in the new list
            var toRemove = orgUnit
                .Managers.Where(um => !request.ManagerIds.Contains(um.EmployeeId))
                .ToList();
            foreach (var um in toRemove)
                orgUnit.Managers.Remove(um);

            // Add new managers
            var existingManagerIds = orgUnit.Managers.Select(um => um.EmployeeId).ToHashSet();
            foreach (var managerId in request.ManagerIds)
            {
                if (!existingManagerIds.Contains(managerId))
                {
                    orgUnit.Managers.Add(new UnitsManagers(orgUnit.Id, managerId));
                }
            }
        }

        await _repository.UpdateAsync(orgUnit, cancellationToken);
    }
}