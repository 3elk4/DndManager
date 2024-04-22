using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class MoneyConfiguration : BaseEntityConfiguration<Money>
    {
        public override void Configure(EntityTypeBuilder<Money> builder)
        {
            base.Configure(builder);

            builder.HasOne(a => a.Pc).WithOne(x => x.Money).HasForeignKey<Money>(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
