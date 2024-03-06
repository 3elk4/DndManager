namespace Application.NpcFeat
{
    public class NpcFeatVMValidator : AbstractValidator<NpcFeatVM>
    {
        public NpcFeatVMValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
            RuleFor(v => v.TimeRegeneration).MaximumLength(100);
            RuleFor(v => v.Description).MaximumLength(1000).NotEmpty();
            RuleFor(v => v.NpcId).NotEmpty();
        }
    }
}
