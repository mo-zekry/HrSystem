using FluentValidation;

namespace HrSystem.Application.Features.Employees.Commands.DeactivateEmployee;

internal sealed class DeactivateEmployeeCommandValidator
    : AbstractValidator<DeactivateEmployeeCommand>
{
    public DeactivateEmployeeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}