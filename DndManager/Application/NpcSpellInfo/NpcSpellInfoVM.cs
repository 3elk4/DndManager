using Application.Common.Models;

namespace Application.NpcSpellInfo
{
    public class NpcSpellInfoVM : BaseVM
    {
        public string? NpcAbilityId { get; set; }

        public int GlobalAttackMod { get; set; }
        public int CasterLvl { get; set; }
        public int SpellSaveDcMod { get; set; }

        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcSpellInfo, NpcSpellInfoVM>();
                CreateMap<NpcSpellInfoVM, Domain.Entities.NpcSpellInfo>();
            }
        }
    }
}
