using Application.NpcAbility;
using Application.NpcSpellInfo;

namespace Application.Npc
{
    public class NpcEditableVMValidator : AbstractValidator<NpcEditableVM>
    {
        public NpcEditableVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
            RuleFor(v => v.Type).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Size).MaximumLength(50).NotEmpty();
            RuleFor(v => v.AC).GreaterThan(0).NotEmpty();
            RuleFor(v => v.HP).GreaterThan(0).NotEmpty();
            RuleFor(v => v.ProficiencyBonus).GreaterThanOrEqualTo(0);
            RuleFor(v => v.PassivePerception).GreaterThanOrEqualTo(0);
            RuleFor(v => v.HpFormula).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Speed).MaximumLength(100).NotEmpty();
            RuleFor(v => v.Alignment).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Challange).GreaterThanOrEqualTo(0);

            RuleFor(v => v.SpellInfo).SetValidator(new NpcSpellInfoVMValidator());
            RuleForEach(v => v.Abilities).SetValidator(new NpcAbilityVMValidator());
        }
    }
}
