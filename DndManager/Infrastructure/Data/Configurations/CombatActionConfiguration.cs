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
    class CombatActionConfiguration : IEntityTypeConfiguration<CombatAction>
    {
        public void Configure(EntityTypeBuilder<CombatAction> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(50).IsRequired();

            builder.OwnsOne(a => a.CombatAttack);
            builder.OwnsOne(a => a.CombatDamage);
            builder.OwnsOne(a => a.CombatSavingThrow);

            builder.HasOne(a => a.Pc).WithMany(x => x.CombatsActions).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
