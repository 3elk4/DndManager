using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class NpcAbility : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int SavingThrowBonus { get; set; }

        public string NpcId { get; set; }
        public Npc Npc { get; set; } = null!;

        public IList<NpcSkill> Skills { get; private set; } = new List<NpcSkill>();
    }
}
