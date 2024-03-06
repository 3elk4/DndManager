using Application.Ability;

namespace Application.CombatAction
{
    public class CombatActionsWithAbilitiesVMValidator : AbstractValidator<CombatActionsWithAbilitiesVM>
    {
        public CombatActionsWithAbilitiesVMValidator()
        {
            RuleFor(v => v.Proficiency).GreaterThanOrEqualTo(2).LessThanOrEqualTo(6).NotEmpty();
            RuleFor(v => v.Abilities).NotEmpty();

            RuleForEach(v => v.Abilities).SetValidator(new AbilityBriefVMValidator());
            RuleForEach(v => v.CombatActions).SetValidator(new CombatActionVMValidator());
        }
    }
}
