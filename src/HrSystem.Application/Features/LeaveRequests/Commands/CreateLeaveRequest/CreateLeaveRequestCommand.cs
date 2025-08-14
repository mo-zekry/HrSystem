using MediatR;

namespace HrSystem.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;

public sealed record CreateLeaveRequestCommand(
    int EmployeeId,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason
) : IRequest<int>;
