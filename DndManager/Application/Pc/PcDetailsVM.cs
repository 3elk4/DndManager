using Application.Ability;
using Application.Bio;
using Application.Common.Extentions;
using Application.Common.Models;
using Application.DndClass;
using Application.SpellInfo;
using System.Collections.Generic;

namespace Application.Pc
{
    public class PcDetailsVM : BaseVM
    {
        public string Name { get; init; }
        public string Image { get; init; }
        public IReadOnlyCollection<DndClassVM> DndClasses { get; init; }
        public IReadOnlyCollection<AbilityVM> Abilities { get; init; }
        public BioVM Bio { get; init; }
        public SpellInfoVM SpellInfo { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }

        public bool Inspiration { get; init; }

        public int AC { get; init; }
        public string Speed { get; init; }
        public int HP { get; init; }
        public int CurrentHP { get; init; }
        public int TempHP { get; init; }
        public string HitDice { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, PcDetailsVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
