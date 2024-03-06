using Application.Ability;
using Application.Bio;
using Application.DndClass;

namespace Application.Pc
{
    public class PcCreatableVMValidator : AbstractValidator<PcCreatableVM>
    {
        public PcCreatableVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.RaceName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.BackgroundName).NotEmpty().MaximumLength(50);
            RuleFor(v => v.AC).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(v => v.Speed).NotEmpty().MaximumLength(45);
            RuleFor(v => v.HP).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(v => v.HitDice).NotEmpty().MaximumLength(45);

            RuleForEach(v => v.Abilities).SetValidator(new AbilityVMValidator());
            RuleForEach(v => v.DndClasses).SetValidator(new DndClassVMValidator());
            RuleFor(v => v.Bio).SetValidator(new BioVMValidator());
        }
    }
}
