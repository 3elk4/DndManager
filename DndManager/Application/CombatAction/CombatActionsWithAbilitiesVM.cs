using AutoMapper;
using System.Collections.Generic;

namespace Application.CombatAction
{
    public class CombatActionsWithAbilitiesVM
    {
        public List<CombatActionVM> CombatActions { get; set; }
        public List<AbilityVM> Abilities { get; set; }

        public int Proficiency { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, CombatActionsWithAbilitiesVM>();
            }
        }
    }
}
