using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class SpellConfiguration : BaseEntityConfiguration<Spell>
    {
        public override void Configure(EntityTypeBuilder<Spell> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(1000).IsRequired();
            builder.Property(a => a.CastingTime).HasMaxLength(100).IsRequired();
            builder.Property(a => a.CastingRange).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Components).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Duration).HasMaxLength(50).IsRequired();

            builder.HasOne(a => a.SpellLvlInfo).WithMany(x => x.Spells).HasForeignKey(x => x.SpellLvlInfoId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
