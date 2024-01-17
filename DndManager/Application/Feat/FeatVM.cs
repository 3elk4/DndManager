using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Feat
{
    public class FeatVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide feat title")]
        [MaxLength(100, ErrorMessage = "Feat title should be min 1 and max 100 length")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please provide feat source")]
        [MaxLength(100, ErrorMessage = "Feat source should be min 1 and max 100 length")]
        public string Source { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide feat source type")]
        [MaxLength(100, ErrorMessage = "Feat source type should be min 1 and max 100 length")]
        public string SourceType { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide feat definition")]
        [MaxLength(1000, ErrorMessage = "Feat definition should be min 1 and max 1000 length")]
        public string Definition { get; set; }

        [Required]
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
