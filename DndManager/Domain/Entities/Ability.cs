using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public CombatAttack? CombatAttack { get; set; }
        public CombatDamage? CombatDamage { get; set; }
        public CombatSavingThrow? CombatSavingThrow { get; set; }
    }
}
