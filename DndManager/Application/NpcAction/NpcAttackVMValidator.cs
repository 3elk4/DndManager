namespace Application.NpcAction
{
    public class NpcAttackVMValidator : AbstractValidator<NpcAttackVM>
    {
        public NpcAttackVMValidator()
        {
            RuleFor(v => v.ToHit).GreaterThanOrEqualTo(0).NotEmpty();
            RuleFor(v => v.Type).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Target).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Range).MaximumLength(50).NotEmpty();
        }
    }
}
