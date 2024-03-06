namespace Application.Feat
{
    public class FeatVMValidator : AbstractValidator<FeatVM>
    {
        public FeatVMValidator()
        {
            RuleFor(v => v.Title).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Source).NotEmpty().MaximumLength(100);
            RuleFor(v => v.SourceType).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Definition).MaximumLength(1000);
            RuleFor(v => v.PcId).NotEmpty();
        }
    }
}
