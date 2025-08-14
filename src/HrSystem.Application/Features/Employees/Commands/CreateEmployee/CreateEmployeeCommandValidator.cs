using FluentValidation;
using HrSystem.Application.Features.Employees.Commands.CreateEmployee;

namespace HrSystem.Application.Features.Employees.Commands.CreateEmployee.Validators;

public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(x => x.HireDate).NotEmpty();
        RuleFor(x => x.OrgUnitId).NotEmpty();
    }
}