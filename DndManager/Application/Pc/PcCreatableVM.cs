using Application.Ability;
using Application.Bio;
using Application.Common.Models;
using Application.DndClass;
using Application.SpellInfo;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Pc
{
    public class PcCreatableVM : BaseVM
    {
        public string Name { get; init; }
        public IFormFile Photo { get; set; }
        public IList<DndClassVM> DndClasses { get; init; }
        public IList<AbilityVM> Abilities { get; init; }
        public BioVM Bio { get; init; }
        public SpellInfoVM SpellInfo { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }
        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public string HitDice { get; init; }
    }
}
