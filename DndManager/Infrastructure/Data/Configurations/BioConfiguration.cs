using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class BioConfiguration : BaseEntityConfiguration<Bio>
    {
        public override void Configure(EntityTypeBuilder<Bio> builder)
        {
            base.Configure(builder);
            //builder.Property(a => a.Age).HasMaxLength(45);
            builder.Property(a => a.Size).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Weight).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Height).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Skin).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Eyes).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Hair).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Alignment).HasMaxLength(45).IsRequired(false);
            builder.Property(a => a.Traits).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Flaws).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Bonds).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Ideals).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Allies).HasMaxLength(500).IsRequired(false);
            builder.Property(a => a.Backstory).HasMaxLength(1000).IsRequired(false);

            builder.HasOne(a => a.Pc).WithOne(x => x.Bio).HasForeignKey<Bio>(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
