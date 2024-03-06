namespace Application.NpcAbility
{
    public class NpcSkillVMValidator : AbstractValidator<NpcSkillVM>
    {
        public NpcSkillVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
            RuleFor(v => v.Bonus).GreaterThanOrEqualTo(0);
        }
    }
}
