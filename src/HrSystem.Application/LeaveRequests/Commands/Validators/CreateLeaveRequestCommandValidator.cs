using FluentValidation;

namespace HrSystem.Application.LeaveRequests.Commands.Validators;

internal sealed class CreateLeaveRequestCommandValidator
    : AbstractValidator<CreateLeaveRequestCommand>
{
    public CreateLeaveRequestCommandValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo(x => x.StartDate);
        RuleFor(x => x.Reason).MaximumLength(1000);
    }
}
