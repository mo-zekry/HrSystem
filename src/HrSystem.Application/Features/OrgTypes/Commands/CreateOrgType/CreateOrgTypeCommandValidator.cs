using FluentValidation;

namespace HrSystem.Application.Features.OrgTypes.Commands.CreateOrgType;

public class CreateOrgTypeCommandValidator : AbstractValidator<CreateOrgTypeCommand>
{
    public CreateOrgTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name must not exceed 100 characters.");
    }
}