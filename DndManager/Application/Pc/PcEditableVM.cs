using Application.Ability;
using Application.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Pc
{
    public class PcEditableVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's name")]
        [StringLength(100, ErrorMessage = "Character's name should be min 1 and max 100 length")]
        public string Name { get; init; }
        public IFormFile Photo { get; set; }
        public IList<AbilityVM> Abilities { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's race")]
        [StringLength(50, ErrorMessage = "Character's race should be min 1 and max 50 length")]
        public string RaceName { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's background")]
        [StringLength(50, ErrorMessage = "Character's background should be min 1 and max 50 length")]
        public string BackgroundName { get; init; }
        public bool Inspiration { get; init; }

        [Required(ErrorMessage = "Please provide character's AC")]
        public int AC { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's speed")]
        public string Speed { get; init; }

        [Required(ErrorMessage = "Please provide character's HP")]
        [Range(1, 5000, ErrorMessage = "Character's HP should be min 1 and max 5000")]
        public int HP { get; init; }

        [Range(1, 5000, ErrorMessage = "Character's Current HP should be min 1 and max 5000")]
        public int CurrentHP { get; init; }

        [Range(0, 5000, ErrorMessage = "Character's Temp HP should be min 0 and max 5000")]
        public int TempHP { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's hit dice")]
        public string HitDice { get; init; }
    }
}
