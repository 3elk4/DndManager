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
    class ProficiencyConfiguration : IEntityTypeConfiguration<Proficiency>
    {
        public void Configure(EntityTypeBuilder<Proficiency> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(100).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Proficiencies).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
