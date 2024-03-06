using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    class DndClassConfiguration : BaseEntityConfiguration<DndClass>
    {
        public override void Configure(EntityTypeBuilder<DndClass> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.Name).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Lvl).IsRequired();
            builder.Property(a => a.SubclassName).HasMaxLength(100).IsRequired(false);

            builder.HasOne(a => a.Pc).WithMany(x => x.DndClasses).HasForeignKey(x => x.PcId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
