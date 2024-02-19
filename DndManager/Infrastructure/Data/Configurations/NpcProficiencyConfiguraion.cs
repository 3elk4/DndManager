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
    class NpcProficiencyConfiguraion : BaseEntityConfiguration<NpcProficiency>
    {
        public override void Configure(EntityTypeBuilder<NpcProficiency> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Range).HasMaxLength(50);

            builder.HasOne(a => a.Npc).WithMany(x => x.Proficiencies).HasForeignKey(x => x.NpcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
