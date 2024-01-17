using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.Spell
{
    public class SpellVM : BaseVM
    {
        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's name")]
        [MaxLength(50, ErrorMessage = "Max length of spell's name is 50")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's description")]
        [MaxLength(1000, ErrorMessage = "Max length of spell's description is 1000")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's casting time")]
        [MaxLength(100, ErrorMessage = "Max length of spell's casting time is 100")]
        public string CastingTime { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's casting range")]
        [MaxLength(50, ErrorMessage = "Max length of spell's casting range is 50")]
        public string CastingRange { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's components")]
        [MaxLength(100, ErrorMessage = "Max length of spell's components is 100")]
        public string Components { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = "Please enter spell's duration")]
        [MaxLength(50, ErrorMessage = "Max length of spell's duration is 50")]
        public string Duration { get; set; }

        [Required]
        public string SpellLvlInfoId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Spell, SpellVM>();
            }
        }
    }
}
