using FluentValidation;

namespace HrSystem.Application.OrgUnits.Commands.Validators;

internal sealed class UpdateOrgUnitCommandValidator : AbstractValidator<UpdateOrgUnitCommand>
{
    public UpdateOrgUnitCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.OrgTypeId).NotEmpty();
        When(x => x.ParentId.HasValue, () => RuleFor(x => x.ParentId!.Value).NotEmpty());

        // ManagerIds is optional; when provided, all must be non-empty
        When(
            x => x.ManagerIds != null,
            () =>
            {
                RuleForEach(x => x.ManagerIds!).NotEmpty();
            }
        );
    }
}
