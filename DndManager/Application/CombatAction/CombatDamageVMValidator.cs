namespace Application.CombatAction
{
    public class CombatDamageVMValidator : AbstractValidator<CombatDamageVM>
    {
        public CombatDamageVMValidator()
        {
            RuleFor(v => v.DamageDice).MaximumLength(50);
            RuleFor(v => v.DamageType).MaximumLength(50);
            RuleFor(v => v.AdditionalBonus).GreaterThanOrEqualTo(0);
        }
    }
}
