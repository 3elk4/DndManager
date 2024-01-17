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
    class SpellLvlInfoConfiguration : IEntityTypeConfiguration<SpellLvlInfo>
    {
        public void Configure(EntityTypeBuilder<SpellLvlInfo> builder)
        {
            builder.Property(a => a.Max).IsRequired(false);
            builder.Property(a => a.Remaining).IsRequired(false);
            builder.Property(a => a.Lvl).IsRequired();

            builder.OwnsMany(a => a.Spells);
            builder.HasOne(a => a.SpellInfo).WithMany(x => x.SpellLvls).HasForeignKey(x => x.SpellInfoId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
