using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class CombatAttackConfiguration : BaseEntityConfiguration<CombatAttack>
    {
        public override void Configure(EntityTypeBuilder<CombatAttack> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Range).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatAttack).HasForeignKey<CombatAttack>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithMany(x => x.CombatAttacks).HasForeignKey(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
