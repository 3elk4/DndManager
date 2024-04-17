using System.Collections.Generic;

namespace Domain.Entities
{
    public class Ability : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Value { get; set; } = 1;
        public bool SavingThrow { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;

        public IList<Skill> Skills { get; private set; } = new List<Skill>();

        public SpellInfo? SpellInfo { get; set; }
        public List<CombatAttack> CombatAttacks { get; set; } = new List<CombatAttack>();
        public List<CombatDamage> CombatDamages { get; set; } = new List<CombatDamage>();
        public List<CombatSavingThrow> CombatSavingThrows { get; set; } = new List<CombatSavingThrow>();
    }
}
