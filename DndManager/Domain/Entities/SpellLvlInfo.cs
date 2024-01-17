using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SpellLvlInfo : BaseAuditableEntity
    {
        public int Max { get; set; } = 0;
        public int Remaining { get; set; } = 0;
        public int Lvl { get; set; }

        public IList<Spell> Spells{ get; private set; } = new List<Spell>();

        public string SpellInfoId { get; set; }
        public SpellInfo SpellInfo { get; set; } = null!;
    }
}
