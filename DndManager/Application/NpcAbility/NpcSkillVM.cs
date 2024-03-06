using Application.Common.Models;

namespace Application.NpcAbility
{
    public class NpcSkillVM : BaseVM
    {
        public string Name { get; set; }
        public int Bonus { get; set; }
        public string NpcAbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcSkill, NpcSkillVM>();
                CreateMap<NpcSkillVM, Domain.Entities.NpcSkill>();
            }
        }
    }
}
