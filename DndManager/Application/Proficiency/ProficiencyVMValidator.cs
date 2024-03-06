namespace Application.Proficiency
{
    public class ProficiencyVMValidator : AbstractValidator<ProficiencyVM>
    {
        public ProficiencyVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Type).NotEmpty();
            RuleFor(v => v.PcId).NotEmpty();
        }
    }
}
