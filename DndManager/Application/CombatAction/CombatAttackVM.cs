using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.CombatAction
{
    public class CombatAttackVM : BaseVM
    {
        public bool IsProficient { get; set; }

        [Range(0, 5000, ErrorMessage = "Attack additonal bonus should be min 0 and max 5000")]
        public int AdditionalBonus { get; set; } = 0;

        [MaxLength(50, ErrorMessage = "Attack additonal bonus should be min 0 and max 50 length")]
        public string Range { get; set; }
        public string CombatActionId { get; set; }
        public string? AbilityId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.CombatAttack, CombatAttackVM>();
            }
        }
    }
}
