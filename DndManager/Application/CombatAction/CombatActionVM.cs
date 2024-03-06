using Application.Common.Models;

namespace Application.CombatAction
{
    public class CombatActionVM : BaseVM
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public CombatAttackVM CombatAttack { get; set; }
        public CombatDamageVM CombatDamage { get; set; }
        public CombatSavingThrowVM CombatSavingThrow { get; set; }
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
