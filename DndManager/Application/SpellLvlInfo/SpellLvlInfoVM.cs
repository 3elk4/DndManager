using Application.Common.Models;
using Application.Spell;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.SpellLvlInfo
{
    public class SpellLvlInfoVM : BaseVM
    {
        [Range(0, 20, ErrorMessage = "Spell Lvl Info's Max should be min 0 and max 20")]
        public int Max { get; set; } = 0;

        [Range(0, 20, ErrorMessage = "Spell Lvl Info's Remaining should be min 0 and max 20")]
        public int Remaining { get; set; } = 0;

        [Range(0, 9, ErrorMessage = "Spell Lvl Info's Lvl should be min 0 and max 9")]
        public int Lvl { get; set; }

        public IReadOnlyList<SpellVM> Spells { get; init; }

        [Required]
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
