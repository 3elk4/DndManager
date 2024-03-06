namespace Domain.Entities
{
    public class NpcSpellInfo : BaseAuditableEntity
    {
        public int CasterLvl { get; set; }
        public int SpellSaveDcMod { get; set; }
        public int GlobalAttackMod { get; set; }


        public string NpcId { get; set; }
        public Npc Npc { get; set; } = null!;


        public string NpcAbilityId { get; set; }
        public NpcAbility Ability { get; set; } = null!;
    }
}
