using System.Collections.Generic;

namespace Domain.Entities
{
    public class SpellLvlInfo : BaseAuditableEntity
    {
        public int Max { get; set; } = 0;
        public int Remaining { get; set; } = 0;
        public int Lvl { get; set; }

        public IList<Spell> Spells { get; private set; } = new List<Spell>();

        public string SpellInfoId { get; set; }
        public SpellInfo SpellInfo { get; set; } = null!;
    }
}
