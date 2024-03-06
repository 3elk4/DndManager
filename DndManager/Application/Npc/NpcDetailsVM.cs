﻿using Application.Common.Extentions;
using Application.Common.Models;
using Application.NpcAbility;
using Application.NpcSpellInfo;
using System.Collections.Generic;

namespace Application.Npc
{
    public class NpcDetailsVM : BaseVM
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public string Size { get; set; }
        public int AC { get; set; }
        public string AcType { get; set; }
        public int HP { get; set; }
        public string HpFormula { get; set; }
        public string Speed { get; set; }
        public int ProficiencyBonus { get; set; }
        public int PassivePerception { get; set; }
        public string Alignment { get; init; }
        public string Image { get; init; }
        public int Challange { get; init; }
        public int ChallangeXp { get; init; }

        public IReadOnlyCollection<NpcAbilityVM> Abilities { get; init; }
        public NpcSpellInfoVM SpellInfo { get; init; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Npc, NpcDetailsVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
