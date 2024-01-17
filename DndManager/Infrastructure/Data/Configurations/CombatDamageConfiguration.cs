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
    class CombatDamageConfiguration : BaseEntityConfiguration<CombatDamage>
    {
        public override void Configure(EntityTypeBuilder<CombatDamage> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.DamageDice).HasMaxLength(50).IsRequired();
            builder.Property(a => a.DamageType).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatDamage).HasForeignKey<CombatDamage>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.CombatDamage).HasForeignKey<CombatDamage>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
