namespace Application.NpcAbility
{
    public class NpcAbilityVMValidator : AbstractValidator<NpcAbilityVM>
    {
        public NpcAbilityVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Value).GreaterThanOrEqualTo(0);
            RuleFor(v => v.SavingThrowBonus).GreaterThanOrEqualTo(0);

            RuleForEach(v => v.Skills).SetValidator(new NpcSkillVMValidator());
        }
    }
}
