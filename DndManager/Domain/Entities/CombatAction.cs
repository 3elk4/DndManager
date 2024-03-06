namespace Domain.Entities
{
    public class CombatAction : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public CombatAttack CombatAttack { get; set; }
        public CombatDamage CombatDamage { get; set; }
        public CombatSavingThrow CombatSavingThrow { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
