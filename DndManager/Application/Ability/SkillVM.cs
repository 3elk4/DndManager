using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Ability
{
    public class SkillVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's skill name")]
        [StringLength(50, ErrorMessage = "Character's skill name should be min 1 and max 50 length")]
        public string Name { get; set; }
        public bool Proficient { get; set; }

        //[Required]
        public string AbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Skill, SkillVM>();
                CreateMap<SkillVM, Domain.Entities.Skill>();
            }
        }
    }
}
