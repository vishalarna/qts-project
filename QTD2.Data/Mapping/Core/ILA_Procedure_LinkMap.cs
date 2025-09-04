using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_Procedure_LinkMap : Common.CommonMap<ILA_Procedure_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_Procedure_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_Procedure_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.Procedure).WithMany(m => m.ILA_Procedure_Links).HasForeignKey(k => k.ProcedureId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.ProcedureId }).IsUnique();
        }
    }
}
