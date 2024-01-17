using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class PcConfiguration : IEntityTypeConfiguration<Pc>
    {
        public void Configure(EntityTypeBuilder<Pc> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.RaceName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.BackgroundName).HasMaxLength(100).IsRequired();
            builder.Property(a => a.AC).IsRequired();
            builder.Property(a => a.Speed).IsRequired();
            builder.Property(a => a.HP).IsRequired();
            builder.Property(a => a.CurrentHP).IsRequired();
            builder.Property(a => a.TempHP).IsRequired();
            builder.Property(a => a.HitDice).IsRequired();
            builder.Property(a => a.Proficiency).IsRequired();

            builder.OwnsMany(a => a.Abilities);
            builder.OwnsMany(a => a.CombatsActions);
            builder.OwnsMany(a => a.Proficiencies);
            builder.OwnsMany(a => a.Items);
            builder.OwnsMany(a => a.Feats);
            builder.OwnsMany(a => a.DndClasses);

            builder.OwnsOne(a => a.SpellInfo);
            builder.OwnsOne(a => a.Bio);
        }
    }
}
