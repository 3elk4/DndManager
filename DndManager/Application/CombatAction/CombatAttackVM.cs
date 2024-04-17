using Application.Common.Models;

namespace Application.CombatAction
{
    public class CombatAttackVM : BaseVM
    {
        public bool IsProficient { get; set; }
        public int AdditionalBonus { get; set; }
        public string Range { get; set; }
        public string CombatActionId { get; set; }
        public string? AbilityId { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Entities.CombatAttack, CombatAttackVM>();
                CreateMap<CombatAttackVM, Domain.Entities.CombatAttack>();
            }
        }
    }
}
