using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class PcConfiguration : BaseEntityConfiguration<Pc>
    {
        public override void Configure(EntityTypeBuilder<Pc> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.RaceName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.BackgroundName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.AC).IsRequired();
            builder.Property(a => a.Speed).IsRequired();
            builder.Property(a => a.HP).IsRequired();
            builder.Property(a => a.CurrentHP).IsRequired();
            builder.Property(a => a.TempHP).IsRequired();
            builder.Property(a => a.HitDice).IsRequired();
        }
    }
}
