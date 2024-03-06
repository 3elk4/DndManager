using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class NpcSkillConfiguration : BaseEntityConfiguration<NpcSkill>
    {
        public override void Configure(EntityTypeBuilder<NpcSkill> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.HasOne(a => a.NpcAbility).WithMany(x => x.Skills).HasForeignKey(x => x.NpcAbilityId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
