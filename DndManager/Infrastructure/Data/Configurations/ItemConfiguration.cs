using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class ItemConfiguration : BaseEntityConfiguration<Item>
    {
        public override void Configure(EntityTypeBuilder<Item> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Quantity).IsRequired();
            builder.Property(a => a.Weight).IsRequired();
            builder.Property(a => a.Notes).HasMaxLength(500).IsRequired();

            builder.HasOne(a => a.Pc).WithMany(x => x.Items).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
