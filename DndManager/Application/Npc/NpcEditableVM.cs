using Application.Common.Models;
using Application.NpcAbility;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Npc
{
    public class NpcEditableVM : BaseVM
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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's challange")]
        public int Challange { get; init; }
        public int ChallangeExp { get; init; }

        public IFormFile Photo { get; set; }

        public IList<NpcAbilityVM> Abilities { get; init; }
    }
}
