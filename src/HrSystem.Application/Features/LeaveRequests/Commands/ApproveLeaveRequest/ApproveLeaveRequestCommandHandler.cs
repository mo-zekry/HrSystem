using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using MediatR;

namespace HrSystem.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest;

internal sealed class ApproveLeaveRequestCommandHandler(IRepository<LeaveRequest> repository)
    : IRequestHandler<ApproveLeaveRequestCommand>
{
    private readonly IRepository<LeaveRequest> _repository = repository;

    public async Task Handle(
        ApproveLeaveRequestCommand request,
        CancellationToken cancellationToken
    )
    {
        var leave =
            await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"LeaveRequest {request.Id} not found");
        leave.Status = HrSystem.Domain.Enums.LeaveRequestStatus.Approved;
        await _repository.UpdateAsync(leave, cancellationToken);
    }
}
