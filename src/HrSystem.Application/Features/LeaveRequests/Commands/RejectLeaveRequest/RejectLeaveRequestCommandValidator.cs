using FluentValidation;

namespace HrSystem.Application.Features.LeaveRequests.Commands.RejectLeaveRequest;

internal sealed class RejectLeaveRequestCommandValidator : AbstractValidator<RejectLeaveRequestCommand>
{
    public RejectLeaveRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}