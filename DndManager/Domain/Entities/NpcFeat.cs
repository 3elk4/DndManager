using Domain.Common;

namespace Domain.Entities
{
    public class NpcFeat : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string TimeRegeneration { get; set; }
        public string Description { get; set; }

        public string NpcId { get; set; }
        public Npc Npc { get; set; } = null!;
    }
}
