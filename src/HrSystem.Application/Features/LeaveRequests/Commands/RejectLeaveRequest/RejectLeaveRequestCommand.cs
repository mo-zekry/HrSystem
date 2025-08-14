using MediatR;

namespace HrSystem.Application.Features.LeaveRequests.Commands.RejectLeaveRequest;

public sealed record RejectLeaveRequestCommand(int Id) : IRequest;
