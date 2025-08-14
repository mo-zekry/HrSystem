using FluentValidation;

namespace HrSystem.Application.Features.OrgTypes.Commands.UpdateOrgType;

public class UpdateOrgTypeValidator : AbstractValidator<UpdateOrgTypeCommand>
{
    public UpdateOrgTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}