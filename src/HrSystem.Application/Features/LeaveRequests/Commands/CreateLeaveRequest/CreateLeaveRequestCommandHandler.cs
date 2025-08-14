using HrSystem.Application.Repositories;
using HrSystem.Domain.Entities;
using HrSystem.Domain.ValueObjects;
using MediatR;

namespace HrSystem.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

internal sealed class CreateLeaveRequestCommandHandler(IRepository<LeaveRequest> repository)
    : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly IRepository<LeaveRequest> _repository = repository;

    public async Task<int> Handle(
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