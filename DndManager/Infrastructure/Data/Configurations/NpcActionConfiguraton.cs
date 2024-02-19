using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcActionConfiguraton : BaseEntityConfiguration<NpcAction>
    {
        public override void Configure(EntityTypeBuilder<NpcAction> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);

            builder.HasOne(a => a.Npc).WithMany(x => x.Actions).HasForeignKey(x => x.NpcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
