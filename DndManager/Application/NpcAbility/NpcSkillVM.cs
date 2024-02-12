using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.NpcAbility
{
    public class NpcSkillVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's skill name")]
        [StringLength(50, ErrorMessage = "Character's skill name should be min 1 and max 50 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide character's skill bonus value")]
        public int Bonus { get; set; }

        //[Required]
        public string NpcAbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcSkill, NpcSkillVM>();
                CreateMap<NpcSkillVM, Domain.Entities.NpcSkill>();
            }
        }
    }
}
