using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcAbilityConfiguration : BaseEntityConfiguration<NpcAbility>
    {
        public override void Configure(EntityTypeBuilder<NpcAbility> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.Npc).WithMany(x => x.Abilities).HasForeignKey(x => x.NpcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
