using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Bio
{
    public class BioVM : BaseVM
    {
        //[Required]
        public string PcId { get; init; }

        [Range(1, 5000, ErrorMessage = "Age should be min 1 and max 5000 length")]
        public int Age { get; set; }

        [MaxLength(45, ErrorMessage = "Size should be min 0 and max 45 length")]
        public string Size { get; set; }

        [MaxLength(45, ErrorMessage = "Weight should be min 0 and max 45 length")]
        public string Weight { get; set; }

        [MaxLength(45, ErrorMessage = "Height should be min 0 and max 45 length")]
        public string Height { get; set; }

        [MaxLength(45, ErrorMessage = "Skin should be min 0 and max 45 length")]
        public string Skin { get; set; }

        [MaxLength(45, ErrorMessage = "Eyes should be min 0 and max 45 length")]
        public string Eyes { get; set; }

        [MaxLength(45, ErrorMessage = "Hair should be min 0 and max 45 length")]
        public string Hair { get; set; }

        [MaxLength(45, ErrorMessage = "Alignment should be min 0 and max 45 length")]
        public string Alignment { get; set; }

        [MaxLength(500, ErrorMessage = "Traits should be min 0 and max 45 length")]
        public string Traits { get; set; }

        [MaxLength(500, ErrorMessage = "Flaws should be min 0 and max 45 length")]
        public string Flaws { get; set; }

        [MaxLength(500, ErrorMessage = "Bonds should be min 0 and max 45 length")]
        public string Bonds { get; set; }

        [MaxLength(500, ErrorMessage = "Ideals should be min 0 and max 45 length")]
        public string Ideals { get; set; }

        [MaxLength(500, ErrorMessage = "Allies should be min 0 and max 45 length")]
        public string Allies { get; set; }

        [MaxLength(1000, ErrorMessage = "Backstory should be min 0 and max 45 length")]
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
