using Application.Common.Models;

namespace Application.Bio
{
    public class BioVM : BaseVM
    {
        public string PcId { get; init; }
        public int Age { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Alignment { get; set; }
        public string Traits { get; set; }
        public string Flaws { get; set; }
        public string Bonds { get; set; }
        public string Ideals { get; set; }
        public string Allies { get; set; }
        public string Backstory { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Bio, BioVM>();
                CreateMap<BioVM, Domain.Entities.Bio>();
            }
        }
    }
}
