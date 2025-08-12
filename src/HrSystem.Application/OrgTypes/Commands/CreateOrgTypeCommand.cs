using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgTypes.Commands;

public sealed record CreateOrgTypeCommand(string Name) : IRequest<Guid>;

internal sealed class CreateOrgTypeCommandHandler(IRepository<OrgType> repository)
    : IRequestHandler<CreateOrgTypeCommand, Guid>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task<Guid> Handle(
        CreateOrgTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new OrgType(request.Name);
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}
