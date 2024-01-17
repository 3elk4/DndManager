using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SpellInfo : BaseAuditableEntity
    {
        public string? AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;

        public IList<SpellLvlInfo> SpellLvls { get; private set; } = new List<SpellLvlInfo>();
    }
}
