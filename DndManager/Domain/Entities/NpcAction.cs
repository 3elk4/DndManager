namespace Domain.Entities
{
    public class NpcAction : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public string NpcId { get; set; }
        public Npc Npc { get; set; } = null!;

        public NpcAttack Attack { get; init; } = new NpcAttack();
        public NpcDamage Damage { get; init; } = new NpcDamage();
    }
}
