namespace Domain.Entities
{
    public class DndClass : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Lvl { get; set; }
        public string SubclassName { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
