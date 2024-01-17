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
    class FeatConfiguration : IEntityTypeConfiguration<Feat>
    {
        public void Configure(EntityTypeBuilder<Feat> builder)
        {
            builder.Property(a => a.Title).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Source).HasMaxLength(100).IsRequired();
            builder.Property(a => a.SourceType).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Definition).HasMaxLength(1000).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Feats).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
