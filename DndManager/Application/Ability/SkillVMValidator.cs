namespace Application.Ability
{
    public class SkillVMValidator : AbstractValidator<SkillVM>
    {
        public SkillVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
        }
    }
}
