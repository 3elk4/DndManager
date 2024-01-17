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
    class CombatAttackConfiguration : IEntityTypeConfiguration<CombatAttack>
    {
        public void Configure(EntityTypeBuilder<CombatAttack> builder)
        {
            builder.Property(a => a.Range).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatAttack).HasForeignKey<CombatAttack>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.CombatAttack).HasForeignKey<CombatAttack>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
