namespace Domain.Entities
{
    public class NpcProficiency : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Range { get; set; }

        public string NpcId { get; set; }
        public Npc Npc { get; set; } = null!;
    }
}
