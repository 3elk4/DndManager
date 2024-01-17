using Application.Ability;
using AutoMapper;
using System.Collections.Generic;

namespace Application.SpellInfo
{
    public class SpellInfoAndAbilitiesVM
    {
        public SpellInfoVM SpellInfo { get; init; }
        public IReadOnlyCollection<AbilityVM> Abilities { get; init; }
        public int Proficiency { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, SpellInfoAndAbilitiesVM>();
            }
        }
    }
}
