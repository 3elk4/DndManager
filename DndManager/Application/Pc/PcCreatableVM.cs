using Application.Ability;
using Application.Bio;
using Application.Common.Models;
using Application.DndClass;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Pc
{
    public class PcCreatableVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's name")]
        [StringLength(100, ErrorMessage = "Character's name should be min 1 and max 100 length")]
        public string Name { get; init; }
        public IFormFile Photo { get; set; }

        public IList<DndClassVM> DndClasses { get; init; }
        public IList<AbilityVM> Abilities { get; init; }

        public BioVM Bio { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's race")]
        [StringLength(50, ErrorMessage = "Character's race should be min 1 and max 50 length")]
        public string RaceName { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's background")]
        [StringLength(50, ErrorMessage = "Character's background should be min 1 and max 50 length")]
        public string BackgroundName { get; init; }

        [Required(ErrorMessage = "Please provide character's AC")]
        public int AC { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's speed")]
        public string Speed { get; init; }

        [Required(ErrorMessage = "Please provide character's HP")]
        [Range(1, 5000, ErrorMessage = "Character's HP should be min 1 and max 5000")]
        public int HP { get; init; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide character's hit dice")]
        public string HitDice { get; init; }
    }
}
