namespace Domain.Entities
{
    public class Spell : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CastingTime { get; set; }
        public string CastingRange { get; set; }
        public string Components { get; set; }
        public string Duration { get; set; }

        public string Target { get; set; }
        public bool Concentration { get; set; }
        public bool Ritual { get; set; }
        public string School { get; set; }
        public string HigherLvl { get; set; }

        public string SpellLvlInfoId { get; set; }
        public SpellLvlInfo SpellLvlInfo { get; set; } = null!;
    }
}
