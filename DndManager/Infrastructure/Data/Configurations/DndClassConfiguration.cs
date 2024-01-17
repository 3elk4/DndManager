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
    class DndClassConfiguration : IEntityTypeConfiguration<DndClass>
    {
        public void Configure(EntityTypeBuilder<DndClass> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Lvl).IsRequired();
            builder.Property(a => a.SubclassName).HasMaxLength(100).IsRequired(false);

            builder.HasOne(a => a.Pc).WithMany(x => x.DndClasses).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
