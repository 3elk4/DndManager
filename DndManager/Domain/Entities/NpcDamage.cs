using Domain.Common;

namespace Domain.Entities
{
    public class NpcDamage : BaseAuditableEntity
    {
        public string DamageDice { get; set; }
        public string DamageType { get; set; }

        public string ActionId { get; set; }
        public NpcAction Action { get; set; } = null!;
    }
}
