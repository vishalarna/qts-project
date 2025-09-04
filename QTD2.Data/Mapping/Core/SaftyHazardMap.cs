using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SaftyHazardMap : Common.CommonMap<SaftyHazard>
    {
        public override void Configure(EntityTypeBuilder<SaftyHazard> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).HasMaxLength(200).IsRequired();
            builder.Property(o => o.RevisionNumber).HasMaxLength(20);
            builder.Property(o => o.Number).HasMaxLength(20).IsRequired();
            builder.Property(o => o.HyperLinks).HasMaxLength(200);
            builder.Property(o => o.Text);

            builder.HasOne(o => o.SaftyHazard_Category).WithMany(m => m.SaftyHazards).HasForeignKey(k => k.SaftyHazardCategoryId).IsRequired();

            //builder.HasIndex(i => new { i.SaftyHazardCategoryId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
        }
    }
}
