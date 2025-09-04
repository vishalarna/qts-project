using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_HistoryMap : Common.CommonMap<SafetyHazard_History>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.SafetyHazard).WithMany(x => x.SafetyHazard_Histories).HasForeignKey(y => y.SafetyHazardId).IsRequired();
        }
    }
}
