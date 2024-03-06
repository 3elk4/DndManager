using Application.Common.Models;
using Application.SpellLvlInfo;
using System.Collections.Generic;

namespace Application.SpellInfo
{
    public class SpellInfoVM : BaseVM
    {
        public string? AbilityId { get; set; }
        public string PcId { get; set; }

        public IReadOnlyList<SpellLvlInfoVM> SpellLvls { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.SpellInfo, SpellInfoVM>();
                CreateMap<SpellInfoVM, Domain.Entities.SpellInfo>();
            }
        }
    }
}
