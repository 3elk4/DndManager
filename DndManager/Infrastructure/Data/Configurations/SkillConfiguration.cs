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
    class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Proficient).IsRequired(false);

            builder.HasOne(a => a.Ability).WithMany(x => x.Skills).HasForeignKey(x => x.AbilityId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
