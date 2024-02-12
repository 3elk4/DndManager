using Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Application.NpcAbility
{
    public class NpcAbilityVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's ability name")]
        [StringLength(50, ErrorMessage = "Character's skill name should be min 1 and max 50 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide character's ability value")]
        public int Value { get; set; }

        [Required(ErrorMessage = "Please provide character's saving throw bonus value")]
        public int SavingThrowBonus { get; set; }

        //[Required]
        public string NpcId { get; set; }

        public IList<NpcSkillVM> Skills { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.NpcAbility, NpcAbilityVM>();
                CreateMap<NpcAbilityVM, Domain.Entities.NpcAbility>();
            }
        }
    }
}
