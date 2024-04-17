using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class CombatDamageConfiguration : BaseEntityConfiguration<CombatDamage>
    {
        public override void Configure(EntityTypeBuilder<CombatDamage> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.DamageDice).HasMaxLength(50).IsRequired();
            builder.Property(a => a.DamageType).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatDamage).HasForeignKey<CombatDamage>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithMany(x => x.CombatDamages).HasForeignKey(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
