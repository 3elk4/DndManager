using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.CombatAction
{
    public class CombatDamageVM : BaseVM
    {
        [MaxLength(50, ErrorMessage = "Damage dice should be min 0 and max 50 length")]
        public string DamageDice { get; set; }

        [MaxLength(50, ErrorMessage = "Damage type should be min 0 and max 50 length")]
        public string DamageType { get; set; }

        [Range(0, 5000, ErrorMessage = "Damage additional bonus should be min 0 and max 5000 length")]
        public int AdditionalBonus { get; set; } = 0;
        public string CombatActionId { get; set; }
        public string? AbilityId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.CombatDamage, CombatDamageVM>();
                CreateMap<CombatDamageVM, Domain.Entities.CombatDamage>();
            }
        }
    }
}
