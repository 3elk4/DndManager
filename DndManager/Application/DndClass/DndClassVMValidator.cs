namespace Application.DndClass
{
    public class DndClassVMValidator : AbstractValidator<DndClassVM>
    {
        public DndClassVMValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Lvl).NotEmpty().LessThanOrEqualTo(20).GreaterThanOrEqualTo(1);
            RuleFor(v => v.SubclassName).MaximumLength(100);
        }
    }
}
