using Application.Common.Models;

namespace Application.NpcFeat
{
    public class NpcFeatVM : BaseVM
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcFeat, NpcFeatVM>();
            }
        }
    }
}
