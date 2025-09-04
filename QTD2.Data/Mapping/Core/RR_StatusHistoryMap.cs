using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class RR_StatusHistoryMap : Common.CommonMap<RR_StatusHistory>
    {
        public override void Configure(EntityTypeBuilder<RR_StatusHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(x => x.RR_StatusHistories).HasForeignKey(y => y.RegulatoryRequirementId).IsRequired();
        }
    }
}
