using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Data.Configurations
{
    class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Value).IsRequired();

            builder.OwnsMany(a => a.Skills);
            builder.HasOne(a => a.Pc).WithMany(x => x.Abilities).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
