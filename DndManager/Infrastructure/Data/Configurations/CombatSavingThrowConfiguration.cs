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
    class CombatSavingThrowConfiguration : IEntityTypeConfiguration<CombatSavingThrow>
    {
        public void Configure(EntityTypeBuilder<CombatSavingThrow> builder)
        {
            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatSavingThrow).HasForeignKey<CombatAttack>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.CombatSavingThrow).HasForeignKey<CombatAttack>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
