using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CombatSavingThrow : BaseAuditableEntity
    {
        public string CombatActionId { get; set; }
        public CombatAction CombatAction { get; set; } = null!;
        public string AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;
    }
}
