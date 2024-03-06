namespace Application.NpcAction
{
    public class NpcDamageVMValidator : AbstractValidator<NpcDamageVM>
    {
        public NpcDamageVMValidator()
        {
            RuleFor(v => v.DamageDice).MaximumLength(50);
            RuleFor(v => v.DamageType).MaximumLength(50);
        }
    }
}
