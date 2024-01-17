using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DndClass.Commands.Create
{
    class AddNewDndClassCommandValidator : AbstractValidator<AddNewDndClassCommand>
    {
        public AddNewDndClassCommandValidator()
        {
            RuleFor(v => v.Name).NotEmpty().MaximumLength(100);
            RuleFor(v => v.Lvl).NotEmpty().LessThanOrEqualTo(20).GreaterThanOrEqualTo(1);
            RuleFor(v => v.SubclassName).MaximumLength(100);
            RuleFor(v => v.PcId).NotEmpty();
        }
    }
}
