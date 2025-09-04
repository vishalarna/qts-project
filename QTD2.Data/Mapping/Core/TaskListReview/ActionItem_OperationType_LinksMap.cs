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
    public class ActionItem_OperationType_LinksMap : Common.CommonMap<ActionItem_OperationType_Links>
    {
        public override void Configure(EntityTypeBuilder<ActionItem_OperationType_Links> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.ActionItemOperationName).IsRequired();
            builder.Property(o => o.OperationTypeId).IsRequired();
            builder.HasOne(o => o.OperationType).WithMany().HasForeignKey(k => k.OperationTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}