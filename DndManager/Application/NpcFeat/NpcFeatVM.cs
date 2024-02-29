using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;


namespace Application.NpcFeat
{
    public class NpcFeatVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide feat name")]
        [MaxLength(100, ErrorMessage = "Feat title should be min 1 and max 100 length")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Feat time regeneration should be min 0 and max 100 length")]
        public string TimeRegeneration { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide feat description")]
        [MaxLength(1000, ErrorMessage = "Feat description should be min 1 and max 1000 length")]
        public string Description { get; set; }
        [Required]
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcFeat, NpcFeatVM>();
            }
        }
    }
}
