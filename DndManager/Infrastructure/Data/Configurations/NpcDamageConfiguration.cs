using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcDamageConfiguration : BaseEntityConfiguration<NpcDamage>
    {
        public override void Configure(EntityTypeBuilder<NpcDamage> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.DamageDice).HasMaxLength(50).IsRequired();
            builder.Property(a => a.DamageType).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.Action).WithOne(x => x.Damage).HasForeignKey<NpcDamage>(x => x.ActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
