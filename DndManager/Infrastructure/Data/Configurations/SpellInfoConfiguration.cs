using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    class SpellInfoConfiguration : IEntityTypeConfiguration<SpellInfo>
    {

        public void Configure(EntityTypeBuilder<SpellInfo> builder)
        {
            builder.OwnsMany(a => a.SpellLvls);

            builder.HasOne(a => a.Pc).WithOne(x => x.SpellInfo).HasForeignKey<SpellInfo>(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.SpellInfo).HasForeignKey<SpellInfo>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
