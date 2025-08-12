using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.LeaveRequests.Commands;

public sealed record RejectLeaveRequestCommand(int Id) : IRequest;

internal sealed class RejectLeaveRequestCommandHandler(IRepository<LeaveRequest> repository)
    : IRequestHandler<RejectLeaveRequestCommand>
{
    private readonly IRepository<LeaveRequest> _repository = repository;

    public async Task Handle(RejectLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leave =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"LeaveRequest {request.Id} not found");
        leave.Status = HrSystem.Domain.Enums.LeaveRequestStatus.Rejected;
        await _repository.UpdateAsync(leave, cancellationToken);
    }
}
