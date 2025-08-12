using FluentValidation;

namespace HrSystem.Application.Employees.Commands.Validators;

internal sealed class DeactivateEmployeeCommandValidator
    : AbstractValidator<DeactivateEmployeeCommand>
{
    public DeactivateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
