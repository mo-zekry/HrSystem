using FluentValidation;

namespace HrSystem.Application.LeaveRequests.Commands.Validators;

internal sealed class RejectLeaveRequestCommandValidator : AbstractValidator<RejectLeaveRequestCommand>
{
    public RejectLeaveRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
