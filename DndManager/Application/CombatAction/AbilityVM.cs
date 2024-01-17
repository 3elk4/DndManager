using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.CombatAction
{
    public class AbilityVM : BaseVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.Ability, AbilityVM>();
            }
        }
    }
}
