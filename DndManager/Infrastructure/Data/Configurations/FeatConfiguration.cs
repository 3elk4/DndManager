using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class FeatConfiguration : BaseEntityConfiguration<Feat>
    {
        public override void Configure(EntityTypeBuilder<Feat> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Title).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Source).HasMaxLength(100).IsRequired();
            builder.Property(a => a.SourceType).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Definition).HasMaxLength(1000).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Feats).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
