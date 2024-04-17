namespace Domain.Entities
{
    public class NpcSkill : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int? Bonus { get; set; } = null;

        public string NpcAbilityId { get; set; }
        public NpcAbility NpcAbility { get; set; } = null!;
    }
}
