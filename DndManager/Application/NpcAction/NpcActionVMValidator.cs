namespace Application.NpcAction
{
    public class NpcActionVMValidator : AbstractValidator<NpcActionVM>
    {
        public NpcActionVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
            RuleFor(v => v.Type).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Description).MaximumLength(500);
            RuleFor(v => v.NpcId).NotEmpty();

            RuleFor(v => v.Attack).SetValidator(new NpcAttackVMValidator());
            RuleFor(v => v.Damage).SetValidator(new NpcDamageVMValidator());
        }
    }
}
