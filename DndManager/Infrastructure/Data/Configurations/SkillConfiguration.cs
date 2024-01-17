using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class SkillConfiguration : BaseEntityConfiguration<Skill>
    {
        public override void Configure(EntityTypeBuilder<Skill> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.HasOne(a => a.Ability).WithMany(x => x.Skills).HasForeignKey(x => x.AbilityId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
