using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcAttackConfiguration : BaseEntityConfiguration<NpcAttack>
    {
        public override void Configure(EntityTypeBuilder<NpcAttack> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Type).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Range).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Target).HasMaxLength(50).IsRequired();
            builder.Property(a => a.ToHit).IsRequired();

            builder.HasOne(a => a.Action).WithOne(x => x.Attack).HasForeignKey<NpcAttack>(x => x.ActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
