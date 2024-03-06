using Application.Ability;

namespace Application.SpellInfo
{
    public class SpellInfoAndAbilitiesVMValidator : AbstractValidator<SpellInfoAndAbilitiesVM>
    {
        public SpellInfoAndAbilitiesVMValidator()
        {
            RuleFor(v => v.Proficiency).NotEmpty().GreaterThanOrEqualTo(2).LessThanOrEqualTo(6);
            RuleFor(v => v.Abilities).NotEmpty();

            RuleForEach(v => v.Abilities).SetValidator(new AbilityBriefVMValidator());
            RuleFor(v => v.SpellInfo).SetValidator(new SpellInfoVMValidator());
        }
    }
}
