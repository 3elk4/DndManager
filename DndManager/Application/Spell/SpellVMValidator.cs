namespace Application.Spell
{
    public class SpellVMValidator : AbstractValidator<SpellVM>
    {
        public SpellVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Duration).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Description).NotEmpty().MaximumLength(1000);
            RuleFor(v => v.CastingTime).NotEmpty().MaximumLength(100);
            RuleFor(v => v.CastingRange).NotEmpty().MaximumLength(50);
            RuleFor(v => v.Components).NotEmpty().MaximumLength(100);
            RuleFor(v => v.SpellLvlInfoId).NotEmpty();
        }
    }
}
