using Application.Common.Models;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Ability
{
    public class AbilityVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's ability name")]
        [StringLength(50, ErrorMessage = "Character's skill name should be min 1 and max 50 length")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide character's ability value")]
        [Range(1, 20, ErrorMessage = "Character's ability value should be min 1 and max 20")]
        public int Value { get; set; }
        public bool SavingThrow { get; set; }

        //[Required]
        public string PcId { get; set; }

        public IList<SkillVM> Skills { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Ability, AbilityVM>();
                CreateMap<AbilityVM, Domain.Entities.Ability>();
            }
        }
    }
}
