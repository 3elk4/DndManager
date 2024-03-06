using Application.Common.Models;
using Application.Spell;
using System.Collections.Generic;

namespace Application.SpellLvlInfo
{
    public class SpellLvlInfoVM : BaseVM
    {
        public int Max { get; set; } = 0;
        public int Remaining { get; set; } = 0;
        public int Lvl { get; set; }

        public IReadOnlyList<SpellVM> Spells { get; init; }
        public string SpellInfoId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.SpellLvlInfo, SpellLvlInfoVM>();
                CreateMap<SpellLvlInfoVM, Domain.Entities.SpellLvlInfo>();
            }
        }
    }
}
