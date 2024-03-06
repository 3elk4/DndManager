using Application.Common.Models;

namespace Application.Feat
{
    public class FeatVM : BaseVM
    {
        public string Title { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Definition { get; set; }
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Feat, FeatVM>();
            }
        }
    }
}
