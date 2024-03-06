using Application.Common.Models;

namespace Application.DndClass
{
    public class DndClassVM : BaseVM
    {
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; } = "";
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.DndClass, DndClassVM>();
                CreateMap<DndClassVM, Domain.Entities.DndClass>();
            }
        }
    }
}
