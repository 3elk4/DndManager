using Application.Common.Extentions;
using Application.Common.Models;

namespace Application.Npc
{
    public class NpcBriefVM : BaseVM
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public string Size { get; set; }
        public string Alignment { get; init; }
        public string Image { get; init; }
        public int Challange { get; init; }
        public int ChallangeXp { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Npc, NpcBriefVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
