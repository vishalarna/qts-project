using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Proc_IssuingAuthority_HistoryMap : Common.CommonMap<Proc_IssuingAuthority_History>
    {
        public override void Configure(EntityTypeBuilder<Proc_IssuingAuthority_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
            builder.Property(o => o.OldStatus).IsRequired();
            builder.Property(o => o.NewStatus).IsRequired();
            builder.Property(o => o.ChangeNotes).HasMaxLength(50).IsRequired();
            builder.HasOne(o => o.Procedure_IssuingAuthority).WithMany(x => x.IssuingAuthorityStatusHistories).HasForeignKey(y => y.ProcedureIssuingAuthorityId).IsRequired();
        }
    }
}
