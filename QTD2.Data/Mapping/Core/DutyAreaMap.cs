using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class DutyAreaMap : Common.CommonMap<DutyArea>
    {
        public override void Configure(EntityTypeBuilder<DutyArea> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.Letter).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.ReasonForRevision);
            builder.Navigation(n => n.SubdutyAreas);
        }
    }
}
