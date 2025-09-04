using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SubDutyArea_HistoryMap : Common.CommonMap<SubDutyArea_History>
    {
        public override void Configure(EntityTypeBuilder<SubDutyArea_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes).IsRequired();
            builder.HasOne(o => o.SubDutyArea).WithMany(x => x.SubDutyArea_Histories).HasForeignKey(y => y.SubDutyAreaId).IsRequired();
        }
    }
}
