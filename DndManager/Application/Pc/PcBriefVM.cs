using Application.Common.Extentions;
using Application.Common.Models;
using Application.DndClass;
using System.Collections.Generic;

namespace Application.Pc
{
    public class PcBriefVM : BaseVM
    {
        public string Name { get; init; }
        public string Image { get; init; }
        public IReadOnlyCollection<DndClassVM> DndClasses { get; init; }
        public string RaceName { get; init; }
        public string BackgroundName { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, PcBriefVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
