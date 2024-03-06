using Application.Common.Models;

namespace Application.Ability
{
    public class SkillVM : BaseVM
    {
        public string Name { get; set; }
        public bool Proficient { get; set; }
        public string AbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Skill, SkillVM>();
                CreateMap<SkillVM, Domain.Entities.Skill>();
            }
        }
    }
}
