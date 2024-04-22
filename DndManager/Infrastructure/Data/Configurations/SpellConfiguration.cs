using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class SpellConfiguration : BaseEntityConfiguration<Spell>
    {
        public override void Configure(EntityTypeBuilder<Spell> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(2500).IsRequired();
            builder.Property(a => a.CastingTime).HasMaxLength(250).IsRequired();
            builder.Property(a => a.CastingRange).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Components).HasMaxLength(250).IsRequired();
            builder.Property(a => a.Duration).HasMaxLength(250).IsRequired();

            builder.Property(a => a.Target).HasMaxLength(250).IsRequired(false);
            builder.Property(a => a.Concentration).IsRequired();
            builder.Property(a => a.Ritual).IsRequired();
            builder.Property(a => a.School).IsRequired();
            builder.Property(a => a.HigherLvl).HasMaxLength(1500).IsRequired(false);

            builder.HasOne(a => a.SpellLvlInfo).WithMany(x => x.Spells).HasForeignKey(x => x.SpellLvlInfoId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
