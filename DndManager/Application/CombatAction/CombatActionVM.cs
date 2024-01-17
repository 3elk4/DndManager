using Application.Common.Models;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Application.CombatAction
{
    public class CombatActionVM : BaseVM
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action name")]
        [MaxLength(50, ErrorMessage = "Action name should be min 1 and max 50 length")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide action type")]
        [MaxLength(50, ErrorMessage = "Action type should be min 1 and max 50 length")]
        public string Type { get; set; }

        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }

        [Required]
        public string PcId { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.CombatAction, CombatActionVM>();
            }
        }
    }
}
