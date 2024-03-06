namespace Application.NpcProficiency
{
    public class NpcProficiencyVMValidator : AbstractValidator<NpcProficiencyVM>
    {
        public NpcProficiencyVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
            RuleFor(v => v.Type).MaximumLength(100).NotEmpty();
            RuleFor(v => v.Range).MaximumLength(50).NotEmpty();
            RuleFor(v => v.NpcId).NotEmpty();
        }
    }
}
