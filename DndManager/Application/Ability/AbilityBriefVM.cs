using Application.Common.Models;

namespace Application.Ability
{
    public class AbilityBriefVM : BaseVM
    {
        public string Name { get; set; }
        public int Value { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Ability, AbilityBriefVM>();
            }
        }
    }
}
