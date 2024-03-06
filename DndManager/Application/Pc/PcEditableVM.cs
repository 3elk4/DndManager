using Application.Ability;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Application.Pc
{
    public class PcEditableVM : BaseVM
    {
        public string Name { get; init; }
        public IFormFile Photo { get; set; }
        public IList<AbilityVM> Abilities { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }
        public bool Inspiration { get; init; }
        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public int CurrentHP { get; init; }
        public int TempHP { get; init; }
        public string HitDice { get; init; }
    }
}
