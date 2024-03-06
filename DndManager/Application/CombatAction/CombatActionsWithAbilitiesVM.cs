using Application.Ability;
using Application.Common.Extentions;
using System.Collections.Generic;

namespace Application.CombatAction
{
    public class CombatActionsWithAbilitiesVM
    {
        public List<CombatActionVM> CombatActions { get; set; }
        public List<AbilityBriefVM> Abilities { get; set; }

        public int Proficiency { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, CombatActionsWithAbilitiesVM>()
                    .ForMember(dest => dest.Proficiency,
                        cfg => cfg.MapFrom(
                            src => src.DndClasses.Proficiency()));
            }
        }
    }
}
