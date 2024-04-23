using Application.Common.Models;

namespace Application.Spell
{
    public class SpellVM : BaseVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CastingTime { get; set; }
        public string CastingRange { get; set; }
        public string Components { get; set; }
        public string Duration { get; set; }
        public string SpellLvlInfoId { get; set; }

        public string Target { get; set; }
        public bool Concentration { get; set; }
        public bool Ritual { get; set; }
        public string School { get; set; }
        public string HigherLvl { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Spell, SpellVM>();
                CreateMap<SpellVM, Domain.Entities.Spell>();
            }
        }
    }
}
