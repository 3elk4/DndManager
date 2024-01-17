using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class AbilityConfiguration : BaseEntityConfiguration<Ability>
    {
        public override void Configure(EntityTypeBuilder<Ability> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Value).IsRequired();

           // builder.OwnsMany(a => a.Skills);
            builder.HasOne(a => a.Pc).WithMany(x => x.Abilities).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
