using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class SpellLvlInfoConfiguration : BaseEntityConfiguration<SpellLvlInfo>
    {
        public override void Configure(EntityTypeBuilder<SpellLvlInfo> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Lvl).IsRequired();
            builder.HasOne(a => a.SpellInfo).WithMany(x => x.SpellLvls).HasForeignKey(x => x.SpellInfoId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
