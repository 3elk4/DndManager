﻿namespace Domain.Entities
{
    public class Proficiency : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public string PcId { get; set; }
        public Pc Pc { get; set; } = null!;
    }
}
