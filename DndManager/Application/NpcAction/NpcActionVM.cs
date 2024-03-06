using Application.Common.Models;

namespace Application.NpcAction
{
    public class NpcActionVM : BaseVM
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public NpcAttackVM Attack { get; set; }
        public NpcDamageVM Damage { get; set; }
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAction, NpcActionVM>();
            }
        }
    }
}
