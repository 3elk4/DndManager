using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class CombatActionConfiguration : BaseEntityConfiguration<CombatAction>
    {
        public override void Configure(EntityTypeBuilder<CombatAction> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(50).IsRequired();

            //builder.OwnsOne(a => a.CombatAttack);
            //builder.OwnsOne(a => a.CombatDamage);
            //builder.OwnsOne(a => a.CombatSavingThrow);

            builder.HasOne(a => a.Pc).WithMany(x => x.CombatActions).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
