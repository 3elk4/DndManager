using Application.Common.Models;
using Application.SpellLvlInfo;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.SpellInfo
{
    public class SpellInfoVM : BaseVM
    {
        public string? AbilityId { get; set; }

        [Required]
        public string PcId { get; set; }

        public IReadOnlyList<SpellLvlInfoVM> SpellLvls { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.SpellInfo, SpellInfoVM>();
            }
        }
    }
}
