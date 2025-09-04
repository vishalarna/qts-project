using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_SaftyHazard_AbatementMap : Common.CommonMap<Version_SaftyHazard_Abatement>
    {
        public override void Configure(EntityTypeBuilder<Version_SaftyHazard_Abatement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number).IsRequired();
            builder.HasOne(o => o.Version_SaftyHazard).WithMany(m => m.Version_SaftyHazard_Abatements).HasForeignKey(k => k.Version_SaftyHazardId).IsRequired();
        }
    }
}
