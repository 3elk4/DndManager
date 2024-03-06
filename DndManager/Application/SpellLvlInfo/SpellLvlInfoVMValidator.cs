using Application.Spell;

namespace Application.SpellLvlInfo
{
    public class SpellLvlInfoVMValidator : AbstractValidator<SpellLvlInfoVM>
    {
        public SpellLvlInfoVMValidator()
        {
            RuleFor(v => v.SpellInfoId).NotEmpty();
            RuleFor(v => v.Lvl).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(9);
            RuleFor(v => v.Remaining).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(20);
            RuleFor(v => v.Max).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(20);

            RuleForEach(v => v.Spells).SetValidator(new SpellVMValidator());
        }
    }
}
