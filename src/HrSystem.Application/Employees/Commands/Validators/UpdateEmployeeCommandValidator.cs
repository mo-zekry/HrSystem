using FluentValidation;

namespace HrSystem.Application.Employees.Commands.Validators;

internal sealed class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        // OrgUnitId is optional; when provided must be a positive value
        When(
            x => x.OrgUnitId.HasValue,
            () =>
            {
                RuleFor(x => x.OrgUnitId!.Value).NotEmpty();
            }
        );
    }
}
