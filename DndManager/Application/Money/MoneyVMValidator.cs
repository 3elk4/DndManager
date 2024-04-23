namespace Application.Money
{
    public class MoneyVMValidator : AbstractValidator<MoneyVM>
    {
        public MoneyVMValidator()
        {
            RuleFor(v => v.Copper).GreaterThanOrEqualTo(0);
            RuleFor(v => v.Silver).GreaterThanOrEqualTo(0);
            RuleFor(v => v.Electrum).GreaterThanOrEqualTo(0);
            RuleFor(v => v.Gold).GreaterThanOrEqualTo(0);
            RuleFor(v => v.Platinum).GreaterThanOrEqualTo(0);
            RuleFor(v => v.PcId).NotEmpty();
        }
    }
}
