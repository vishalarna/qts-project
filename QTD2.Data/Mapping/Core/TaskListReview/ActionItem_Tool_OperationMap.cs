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
    public class ActionItem_Tool_OperationMap : Common.CommonMap<ActionItem_Tool_Operation>
    {
        public override void Configure(EntityTypeBuilder<ActionItem_Tool_Operation> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ActionItemId).IsRequired();
            builder.Property(o => o.OperationTypeId).IsRequired();
            builder.Property(o => o.ToolId);
            builder.HasOne(o => o.ActionItem).WithMany(o=> o.ActionItem_Tool_Operations).HasForeignKey(k => k.ActionItemId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.OperationType).WithMany().HasForeignKey(k => k.OperationTypeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Tool).WithMany().HasForeignKey(k => k.ToolId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}