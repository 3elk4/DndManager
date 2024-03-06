namespace Application.CombatAction
{
    public class CombatActionVMValidator : AbstractValidator<CombatActionVM>
    {
        public CombatActionVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Type).MaximumLength(50).NotEmpty();
            RuleFor(v => v.PcId).NotEmpty();

            RuleFor(v => v.CombatAttack).SetValidator(new CombatAttackVMValidator());
            RuleFor(v => v.CombatDamage).SetValidator(new CombatDamageVMValidator());
        }
    }
}
