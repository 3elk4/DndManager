namespace Application.Item
{
    public class ItemVMValidator : AbstractValidator<ItemVM>
    {
        public ItemVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Quantity).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(v => v.Weight).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(v => v.Notes).MaximumLength(500);
            RuleFor(v => v.PcId).NotEmpty();
        }
    }
}
