using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Proficiency
{
    public class ProficiencyVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide proficiency name")]
        [MaxLength(100, ErrorMessage = "Proficiency name should be min 1 and max 100 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide proficiency type")]
        public string Type { get; set; }

        [Required]
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Proficiency, ProficiencyVM>();
            }
        }
    }
}
