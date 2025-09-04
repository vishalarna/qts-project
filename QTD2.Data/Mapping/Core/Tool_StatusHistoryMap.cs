using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Tool_StatusHistoryMap : Common.CommonMap<Tool_StatusHistory>
    {
        public override void Configure(EntityTypeBuilder<Tool_StatusHistory> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Tool).WithMany(m => m.Tool_StatusHistories).HasForeignKey(k => k.ToolId).IsRequired();
            builder.Property(o => o.ChangeNotes);
            builder.Property(o => o.ChangeEffectiveDate).IsRequired();
        }
    }
}
