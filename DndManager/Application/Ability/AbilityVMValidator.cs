namespace Application.Ability
{
    public class AbilityVMValidator : AbstractValidator<AbilityVM>
    {
        public AbilityVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Value).GreaterThanOrEqualTo(1).LessThanOrEqualTo(20).NotEmpty();

            RuleForEach(v => v.Skills).SetValidator(new SkillVMValidator());
        }
    }
}
