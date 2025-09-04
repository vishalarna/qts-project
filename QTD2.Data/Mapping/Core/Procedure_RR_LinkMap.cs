using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    internal class Procedure_RR_LinkMap : Common.CommonMap<Procedure_RR_Link>
    {
        public override void Configure(EntityTypeBuilder<Procedure_RR_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(m => m.Procedure_RegRequirement_Links).HasForeignKey(k => k.RegulatoryRequirementId).IsRequired();
            builder.HasOne(o => o.Procedure).WithMany(m => m.Procedure_RegRequirement_Links).HasForeignKey(k => k.ProcedureId).IsRequired();

            builder.HasIndex(i => new { i.RegulatoryRequirementId, i.ProcedureId }).IsUnique();
        }
    }
}
