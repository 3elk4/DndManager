using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class SpellInfoConfiguration : BaseEntityConfiguration<SpellInfo>
    {
        public override void Configure(EntityTypeBuilder<SpellInfo> builder)
        {
            base.Configure(builder);

            builder.HasOne(a => a.Pc).WithOne(x => x.SpellInfo).HasForeignKey<SpellInfo>(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.SpellInfo).HasForeignKey<SpellInfo>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
