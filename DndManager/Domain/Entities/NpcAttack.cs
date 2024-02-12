using Domain.Common;

namespace Domain.Entities
{
    public class NpcAttack : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public int ToHit { get; set; }

        public string NpcActionId { get; set; }
        public NpcAction Action { get; set; } = null!;
    }
}
