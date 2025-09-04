using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SaftyHazard_AbatementMap : Common.CommonMap<SaftyHazard_Abatement>
    {
        public override void Configure(EntityTypeBuilder<SaftyHazard_Abatement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.SaftyHazard_Abatements).HasForeignKey(k => k.SaftyHazardId).IsRequired();

            builder.HasIndex(i => new { i.SaftyHazardId, i.Number }).IsUnique().HasFilter("[Number] IS NOT NULL");
        }
    }
}
