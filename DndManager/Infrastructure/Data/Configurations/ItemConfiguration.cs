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
    class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Quantity).IsRequired();
            builder.Property(a => a.Weight).IsRequired();
            builder.Property(a => a.Notes).HasMaxLength(500).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Items).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
