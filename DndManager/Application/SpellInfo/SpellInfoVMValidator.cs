using Application.SpellLvlInfo;

namespace Application.SpellInfo
{
    public class SpellInfoVMValidator : AbstractValidator<SpellInfoVM>
    {
        public SpellInfoVMValidator()
        {
            RuleForEach(v => v.SpellLvls).SetValidator(new SpellLvlInfoVMValidator());
        }
    }
}
