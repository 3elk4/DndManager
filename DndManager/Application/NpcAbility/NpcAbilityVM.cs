using Application.Common.Models;
using System.Collections.Generic;

namespace Application.NpcAbility
{
    public class NpcAbilityVM : BaseVM
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int SavingThrowBonus { get; set; }
        public string NpcId { get; set; }

        public IList<NpcSkillVM> Skills { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAbility, NpcAbilityVM>();
                CreateMap<NpcAbilityVM, Domain.Entities.NpcAbility>();
            }
        }
    }
}
