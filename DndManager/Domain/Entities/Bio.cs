namespace Domain.Entities
{
    public class Bio : BaseAuditableEntity
    {
        public int Age { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Skin { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Alignment { get; set; }
        public string Traits { get; set; }
        public string Flaws { get; set; }
        public string Bonds { get; set; }
        public string Ideals { get; set; }
        public string Allies { get; set; }
        public string Backstory { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
