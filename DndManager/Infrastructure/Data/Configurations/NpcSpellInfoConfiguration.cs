using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcSpellInfoConfiguration : BaseEntityConfiguration<NpcSpellInfo>
    {
        public override void Configure(EntityTypeBuilder<NpcSpellInfo> builder)
        {
            base.Configure(builder);

            builder.HasOne(a => a.Npc).WithOne(x => x.SpellInfo).HasForeignKey<NpcSpellInfo>(x => x.NpcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.SpellInfo).HasForeignKey<NpcSpellInfo>(x => x.NpcAbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
