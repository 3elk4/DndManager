namespace Domain.Entities
{
    public class Skill : BaseAuditableEntity
    {
        public string Name { get; set; }
        public bool Proficient { get; set; }

        public string AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;
    }
}
