using FluentValidation;

namespace HrSystem.Application.Features.Employees.Commands.UpdateEmployee.Validators;

internal sealed class DeactivateEmployeeCommandValidator
    : AbstractValidator<DeactivateEmployeeCommand>
{
    public DeactivateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}