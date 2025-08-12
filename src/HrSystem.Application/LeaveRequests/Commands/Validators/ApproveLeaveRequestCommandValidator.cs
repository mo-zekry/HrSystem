using FluentValidation;

namespace HrSystem.Application.LeaveRequests.Commands.Validators;

internal sealed class ApproveLeaveRequestCommandValidator
    : AbstractValidator<ApproveLeaveRequestCommand>
{
    public ApproveLeaveRequestCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
