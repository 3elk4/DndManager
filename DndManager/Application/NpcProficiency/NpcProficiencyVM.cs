using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.NpcProficiency
{
    public class NpcProficiencyVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please provide proficiency name")]
        [MaxLength(100, ErrorMessage = "Proficiency name should be min 1 and max 100 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide proficiency type")]
        [MaxLength(100, ErrorMessage = "Proficiency type should be min 1 and max 100 length")]
        public string Type { get; set; }

        [MaxLength(50, ErrorMessage = "Proficiency name should be min 0 and max 50 length")]
        public string Range { get; set; }

        [Required]
        public string NpcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcProficiency, NpcProficiencyVM>();
            }
        }
    }
}
