using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Data.Mapping.Common;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Procedure_StatusHistoryMap : CommonMap<Procedure_StatusHistory>
    {
        public override void Configure(EntityTypeBuilder<Procedure_StatusHistory> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.Procedure).WithMany(x => x.Procedure_StatusHistories).HasForeignKey(y => y.ProcedureId).IsRequired();
        }
    }
}
