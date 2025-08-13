using FluentValidation;

namespace HrSystem.Application.OrgUnits.Commands.Validators;

internal sealed class CreateOrgUnitCommandValidator : AbstractValidator<CreateOrgUnitCommand>
{
    public CreateOrgUnitCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.OrgTypeId).NotEmpty();
        // ParentId is optional; when provided must be non-empty
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
