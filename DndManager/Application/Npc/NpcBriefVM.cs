using Application.Common.Extentions;
using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Npc
{
    public class NpcBriefVM : BaseVM
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's name")]
        [StringLength(100, ErrorMessage = "Character's name should be min 1 and max 100 length")]
        public string Name { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's type")]
        [StringLength(50, ErrorMessage = "Character's type should be min 1 and max 50 length")]
        public string Type { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's size")]
        [StringLength(50, ErrorMessage = "Character's size should be min 1 and max 50 length")]
        public string Size { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's alignment")]
        [StringLength(50, ErrorMessage = "Character's alignment should be min 1 and max 50 length")]
        public string Alignment { get; init; }
        public string Image { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's challange")]
        public int Challange { get; init; }
        public int ChallangeExp { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Npc, NpcBriefVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
