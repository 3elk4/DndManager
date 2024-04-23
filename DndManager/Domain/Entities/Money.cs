namespace Domain.Entities
{
    public class Money : BaseAuditableEntity
    {
        public int Copper { get; set; }
        public int Silver { get; set; }
        public int Electrum { get; set; }
        public int Gold { get; set; }
        public int Platinum { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
