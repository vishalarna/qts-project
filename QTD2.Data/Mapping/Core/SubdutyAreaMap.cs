using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SubdutyAreaMap : Common.CommonMap<SubdutyArea>
    {
        public override void Configure(EntityTypeBuilder<SubdutyArea> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description);
            builder.Property(o => o.SubNumber).IsRequired();
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.ReasonForRevision);
            builder.Property(o => o.EffectiveDate);
            builder.HasOne(o => o.DutyArea).WithMany(m => m.SubdutyAreas).HasForeignKey(o => o.DutyAreaId).IsRequired();

            builder.Navigation(n => n.Tasks).AutoInclude();
        }
    }
}
