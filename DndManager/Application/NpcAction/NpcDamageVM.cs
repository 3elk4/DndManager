using Application.Common.Models;

namespace Application.NpcAction
{
    public class NpcDamageVM : BaseVM
    {
        public string DamageDice { get; set; }
        public string DamageType { get; set; }
        public string ActionId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcDamage, NpcDamageVM>();
                CreateMap<NpcDamageVM, Domain.Entities.NpcDamage>();
            }
        }
    }
}
