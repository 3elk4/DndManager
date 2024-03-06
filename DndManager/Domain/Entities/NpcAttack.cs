namespace Domain.Entities
{
    public class NpcAttack : BaseAuditableEntity
    {
        public string Type { get; set; }
        public string Range { get; set; }
        public string Target { get; set; }
        public int ToHit { get; set; }

        public string ActionId { get; set; }
        public NpcAction Action { get; set; } = null!;
    }
}
