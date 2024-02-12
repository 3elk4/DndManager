using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bio.Commands.Update
{
    class UpdateBioCommandValidator : AbstractValidator<UpdateBioCommand>
    {
        //public UpdateBioCommandValidator()
        //{
        //    RuleFor(v => v.Age).GreaterThan(0);
        //    RuleFor(v => v.Size).MaximumLength(45);
        //    RuleFor(v => v.Weight).MaximumLength(45);
        //    RuleFor(v => v.Height).MaximumLength(45);
        //    RuleFor(v => v.Skin).MaximumLength(45);
        //    RuleFor(v => v.Eyes).MaximumLength(45);
        //    RuleFor(v => v.Hair).MaximumLength(45);
        //    RuleFor(v => v.Alignment).MaximumLength(45);
        //    RuleFor(v => v.Traits).MaximumLength(500);
        //    RuleFor(v => v.Flaws).MaximumLength(500);
        //    RuleFor(v => v.Bonds).MaximumLength(500);
        //    RuleFor(v => v.Ideals).MaximumLength(500);
        //    RuleFor(v => v.Allies).MaximumLength(500);
        //    RuleFor(v => v.Backstory).MaximumLength(1000);
        //    RuleFor(v => v.Id).NotEmpty();
        //}
    }
}
