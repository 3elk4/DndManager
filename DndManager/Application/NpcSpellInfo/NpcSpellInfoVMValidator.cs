namespace Application.NpcSpellInfo
{
    public class NpcSpellInfoVMValidator : AbstractValidator<NpcSpellInfoVM>
    {
        public NpcSpellInfoVMValidator()
        {
            RuleFor(v => v.GlobalAttackMod).GreaterThanOrEqualTo(0);
            RuleFor(v => v.CasterLvl).GreaterThanOrEqualTo(0);
            RuleFor(v => v.SpellSaveDcMod).GreaterThanOrEqualTo(0);
        }
    }
}
