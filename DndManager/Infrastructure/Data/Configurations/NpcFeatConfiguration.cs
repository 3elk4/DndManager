using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcFeatConfiguration : BaseEntityConfiguration<NpcFeat>
    {
        public override void Configure(EntityTypeBuilder<NpcFeat> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(2500).IsRequired();
            builder.Property(a => a.TimeRegeneration).HasMaxLength(250);

            builder.HasOne(a => a.Npc).WithMany(x => x.Feats).HasForeignKey(x => x.NpcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
