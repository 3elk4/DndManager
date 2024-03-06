using Application.Common.Models;

namespace Application.Item
{
    public class ItemVM : BaseVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; }
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Item, ItemVM>();
            }
        }
    }
}
