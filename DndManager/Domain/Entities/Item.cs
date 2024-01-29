using Domain.Common;

namespace Domain.Entities
{
    public class Item : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public string Notes { get; set; } = "";

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
