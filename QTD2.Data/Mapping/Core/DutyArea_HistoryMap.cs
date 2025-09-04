using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class DutyArea_HistoryMap : Common.CommonMap<DutyArea_History>
    {
        public override void Configure(EntityTypeBuilder<DutyArea_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.DutyArea).WithMany(x => x.DutyArea_Histories).HasForeignKey(y => y.DutyAreaId).IsRequired();
        }
    }
}
