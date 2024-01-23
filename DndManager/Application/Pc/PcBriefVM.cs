using Application.Common.Extentions;
using Application.Common.Models;
using Application.DndClass;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Pc
{
    public class PcBriefVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's name")]
        [StringLength(100, ErrorMessage = "Character's name should be min 1 and max 100 length")]
        public string Name { get; init; }
        public string Image { get; init; }
        public IReadOnlyCollection<DndClassVM> DndClasses { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's race")]
        [StringLength(50, ErrorMessage = "Character's race should be min 1 and max 50 length")]
        public string RaceName { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's background")]
        [StringLength(50, ErrorMessage = "Character's background should be min 1 and max 50 length")]
        public string BackgroundName { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Pc, PcBriefVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
