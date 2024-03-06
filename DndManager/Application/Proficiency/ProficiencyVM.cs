using Application.Common.Models;

namespace Application.Proficiency
{
    public class ProficiencyVM : BaseVM
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Proficiency, ProficiencyVM>();
            }
        }
    }
}
