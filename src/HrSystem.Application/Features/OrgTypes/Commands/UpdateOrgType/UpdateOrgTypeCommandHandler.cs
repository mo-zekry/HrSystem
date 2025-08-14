using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Commands.UpdateOrgType;

internal sealed class UpdateOrgTypeCommandHandler(IRepository<OrgType> repository)
    : IRequestHandler<UpdateOrgTypeCommand>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task Handle(UpdateOrgTypeCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"OrgType {request.Id} not found");

        entity.Name = request.Name;
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
