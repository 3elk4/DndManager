using Application.Common.Models;

namespace Application.NpcAction
{
    public class NpcAttackVM : BaseVM
    {
        public int ToHit { get; set; }
        public string Type { get; set; }
        public string Target { get; set; }
        public string Range { get; set; }

        public string ActionId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAttack, NpcAttackVM>();
                CreateMap<NpcAttackVM, Domain.Entities.NpcAttack>();
            }
        }
    }
}