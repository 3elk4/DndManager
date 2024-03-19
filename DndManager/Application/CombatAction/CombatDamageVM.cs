using Application.Common.Models;

namespace Application.CombatAction
{
    public class CombatDamageVM : BaseVM
    {
        public string DamageDice { get; set; }
        public string DamageType { get; set; }
        public int AdditionalBonus { get; set; }
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
