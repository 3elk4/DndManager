﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class CombatSavingThrowConfiguration : BaseEntityConfiguration<CombatSavingThrow>
    {
        public override void Configure(EntityTypeBuilder<CombatSavingThrow> builder)
        {
            base.Configure(builder);

            builder.HasOne(a => a.CombatAction).WithOne(x => x.CombatSavingThrow).HasForeignKey<CombatSavingThrow>(x => x.CombatActionId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Ability).WithOne(x => x.CombatSavingThrow).HasForeignKey<CombatSavingThrow>(x => x.AbilityId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
