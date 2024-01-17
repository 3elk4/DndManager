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
    class BioConfiguration : IEntityTypeConfiguration<Bio>
    {
        public void Configure(EntityTypeBuilder<Bio> builder)
        {
            builder.Property(a => a.Size).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Weight).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Height).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Skin).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Eyes).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Hair).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Alignment).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Traits).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Flaws).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Bonds).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Ideals).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Allies).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Backstory).HasMaxLength(1000).IsRequired(false);

            builder.HasOne(a => a.Pc).WithOne(x => x.Bio).HasForeignKey<Bio>(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
