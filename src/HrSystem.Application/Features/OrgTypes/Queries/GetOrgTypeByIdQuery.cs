using HrSystem.Application.Dtos.Mapping;
using HrSystem.Application.Dtos.OrgTypes;
using HrSystem.Application.Repositories;
using HrSystem.Application.Specifications.OrgTypes;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.OrgTypes.Queries;

public sealed record GetOrgTypeByIdQuery(int Id) : IRequest<OrgTypeDto?>;

internal sealed class GetOrgTypeByIdQueryHandler(IRepository<OrgType> repository)
    : IRequestHandler<GetOrgTypeByIdQuery, OrgTypeDto?>
{
    private readonly IRepository<OrgType> _repository = repository;

    public async Task<OrgTypeDto?> Handle(
        GetOrgTypeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var spec = new OrgTypeByIdSpecification(request.Id);
        var list = await _repository.ListAsync(spec, cancellationToken);
        return list.FirstOrDefault()?.ToDto();
    }
}
