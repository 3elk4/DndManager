using Application.Common.Models;

namespace Application.NpcProficiency
{
    public class NpcProficiencyVM : BaseVM
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcProficiency, NpcProficiencyVM>();
            }
        }
    }
}
