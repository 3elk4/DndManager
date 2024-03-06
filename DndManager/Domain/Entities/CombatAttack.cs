namespace Domain.Entities
{
    public class CombatAttack : BaseAuditableEntity
    {
        public bool IsProficient { get; set; }
        public int AdditionalBonus { get; set; } = 0;
        public string Range { get; set; }

        public string CombatActionId { get; set; }
        public CombatAction CombatAction { get; set; } = null!;
        public string? AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;
    }
}
