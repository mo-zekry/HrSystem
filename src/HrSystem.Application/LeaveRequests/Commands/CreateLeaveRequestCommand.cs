using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.ValueObjects;
using MediatR;

namespace HrSystem.Application.LeaveRequests.Commands;

public sealed record CreateLeaveRequestCommand(
    Guid EmployeeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason
) : IRequest<Guid>;

internal sealed class CreateLeaveRequestCommandHandler(IRepository<LeaveRequest> repository)
    : IRequestHandler<CreateLeaveRequestCommand, Guid>
{
    private readonly IRepository<LeaveRequest> _repository = repository;

    public async Task<Guid> Handle(
        CreateLeaveRequestCommand request,
        CancellationToken cancellationToken
    )
    {
        var period = new DateRange(request.StartDate, request.EndDate);
        var leaveRequest = new LeaveRequest(request.EmployeeId, period, request.Reason);
        await _repository.AddAsync(leaveRequest, cancellationToken);
        return leaveRequest.Id;
    }
}
