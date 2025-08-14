using MediatR;

namespace HrSystem.Application.Features.LeaveRequests.Commands.ApproveLeaveRequest;

public sealed record ApproveLeaveRequestCommand(int Id) : IRequest;