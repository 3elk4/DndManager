using Application.Common.Models;

namespace Application.Money
{
    public class MoneyVM : BaseVM
    {
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Electrum { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Money, MoneyVM>();
            }
        }
    }
}
