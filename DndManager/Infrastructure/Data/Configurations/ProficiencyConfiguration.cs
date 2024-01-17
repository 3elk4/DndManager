using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class ProficiencyConfiguration : BaseEntityConfiguration<Proficiency>
    {
        public override void Configure(EntityTypeBuilder<Proficiency> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(100).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Proficiencies).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
