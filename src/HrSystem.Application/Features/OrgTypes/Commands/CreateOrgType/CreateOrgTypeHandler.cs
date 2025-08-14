using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.OrgTypes.Commands.CreateOrgType;

internal sealed class CreateOrgTypeCommandHandler(IRepository<OrgType> repository)
    : IRequestHandler<CreateOrgTypeCommand, int>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task<int> Handle(
        CreateOrgTypeCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = new OrgType(request.Name);
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }
}