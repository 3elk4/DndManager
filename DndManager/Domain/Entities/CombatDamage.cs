namespace Domain.Entities
{
    public class CombatDamage : BaseAuditableEntity
    {
        public string DamageDice { get; set; }
        public string DamageType { get; set; }
        public int AdditionalBonus { get; set; } = 0;

        public string CombatActionId { get; set; }
        public CombatAction CombatAction { get; set; } = null!;
        public string AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;
    }
}
