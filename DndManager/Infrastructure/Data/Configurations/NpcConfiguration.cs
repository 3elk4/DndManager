using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcConfiguration : BaseEntityConfiguration<Npc>
    {
        public override void Configure(EntityTypeBuilder<Npc> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Size).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Alignment).HasMaxLength(50).IsRequired();
            builder.Property(a => a.AC).IsRequired();
            builder.Property(a => a.Speed).IsRequired();
            builder.Property(a => a.HP).IsRequired();
            builder.Property(a => a.HpFormula).HasMaxLength(50).IsRequired();
            builder.Property(a => a.PassivePerception).IsRequired();
            builder.Property(a => a.ProficiencyBonus).IsRequired();
            builder.Property(a => a.Challange).IsRequired();
        }
    }
}
