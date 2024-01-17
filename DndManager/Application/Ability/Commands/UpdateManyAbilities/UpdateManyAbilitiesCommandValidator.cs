using FluentValidation;

namespace Application.Ability.Commands.UpdateManyAbilities
{
    public class UpdateManyAbilitiesCommandValidator : AbstractValidator<UpdateManyAbilitiesCommand>
    {
        //private readonly IRepository<Ability> repository;

        //public UpdateManyAbilitiesCommandValidator(IRepository context)
        //{
        //    _context = context;

        //    RuleFor(v => v.Abilities)
        //        .NotEmpty()
        //        .MaximumLength(200)
        //        .MustAsync(BeUniqueTitle)
        //            .WithMessage("'{PropertyName}' must be unique.")
        //            .WithErrorCode("Unique");
        //}

        //public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        //{
        //    return await _context.TodoLists
        //        .AllAsync(l => l.Title != title, cancellationToken);
        //}
    }
}
