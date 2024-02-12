using Application.Common.Extentions;
using Application.Common.Models;
using Application.NpcAbility;
using Application.NpcSpellInfo;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Npc
{
    public class NpcDetailsVM : BaseVM
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's AC")]
        public int AC { get; set; }
        public string AcType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's HP")]
        public int HP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's HP formula")]
        [StringLength(50, ErrorMessage = "Character's HP formula should be min 1 and max 50 length")]
        public string HpFormula { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's speed")]
        [StringLength(100, ErrorMessage = "Character's speed should be min 1 and max 100 length")]
        public string Speed { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's proficiency bonus ")]
        public int ProficiencyBonus { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's passive perception")]
        public int PassivePerception { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's alignment")]
        [StringLength(50, ErrorMessage = "Character's alignment should be min 1 and max 50 length")]
        public string Alignment { get; init; }
        public string Image { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's challange")]
        public int Challange { get; init; }
        public int ChallangeXp { get; init; }

        public IReadOnlyCollection<NpcAbilityVM> Abilities { get; init; }
        public NpcSpellInfoVM SpellInfo { get; init; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Npc, NpcDetailsVM>()
                    .ForMember(dest => dest.Image,
                               cfg => cfg.MapFrom(
                                   src => src.Image.ConvertToBase64String()));
            }
        }
    }
}
