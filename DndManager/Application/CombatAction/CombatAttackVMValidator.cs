namespace Application.CombatAction
{
    public class CombatAttackVMValidator : AbstractValidator<CombatAttackVM>
    {
        public CombatAttackVMValidator()
        {
            RuleFor(v => v.AdditionalBonus).GreaterThanOrEqualTo(0);
            RuleFor(v => v.Range).MaximumLength(50);
        }
    }
}
