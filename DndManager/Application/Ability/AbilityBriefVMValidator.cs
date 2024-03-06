namespace Application.Ability
{
    public class AbilityBriefVMValidator : AbstractValidator<AbilityBriefVM>
    {
        public AbilityBriefVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Value).GreaterThanOrEqualTo(1).LessThanOrEqualTo(20).NotEmpty();
        }
    }
}
