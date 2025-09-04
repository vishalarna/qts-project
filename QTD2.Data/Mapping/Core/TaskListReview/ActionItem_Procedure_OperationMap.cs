using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ActionItem_Procedure_OperationMap : Common.CommonMap<ActionItem_Procedure_Operation>
    {
        public override void Configure(EntityTypeBuilder<ActionItem_Procedure_Operation> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ActionItemId).IsRequired();
            builder.Property(o => o.OperationTypeId).IsRequired();
            builder.Property(o => o.ProcedureId);
            builder.HasOne(o => o.ActionItem).WithMany(o=> o.ActionItem_Procedure_Operations).HasForeignKey(k => k.ActionItemId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.OperationType).WithMany().HasForeignKey(k => k.OperationTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Procedure).WithMany().HasForeignKey(k => k.ProcedureId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}