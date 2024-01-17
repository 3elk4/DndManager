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
    class SpellConfiguration : IEntityTypeConfiguration<Spell>
    {
        public void Configure(EntityTypeBuilder<Spell> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.CastingTime).HasMaxLength(100).IsRequired();
            builder.Property(a => a.CastingRange).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Components).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Duration).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.SpellLvlInfo).WithMany(x => x.Spells).HasForeignKey(x => x.SpellLvlInfoId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
