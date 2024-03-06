using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Ability
{
    public class AbilityVM : BaseVM
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool SavingThrow { get; set; }
        public string PcId { get; set; }

        public IList<SkillVM> Skills { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Ability, AbilityVM>();
                CreateMap<AbilityVM, Domain.Entities.Ability>();
            }
        }
    }
}
