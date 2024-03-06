using Application.Ability;

namespace Application.Pc
{
    public class PcEditableVMValidator : AbstractValidator<PcEditableVM>
    {
        public PcEditableVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.RaceName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.BackgroundName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.AC).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(v => v.Speed).NotEmpty().MaximumLength(45);
            RuleFor(v => v.HP).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(v => v.HitDice).NotEmpty().MaximumLength(45);
            RuleFor(v => v.CurrentHP).GreaterThanOrEqualTo(0);
            RuleFor(v => v.TempHP).GreaterThanOrEqualTo(0);

            RuleForEach(v => v.Abilities).SetValidator(new AbilityVMValidator());
        }
    }
}
