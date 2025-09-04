using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_ToolMap : Common.CommonMap<EnablingObjective_Tool>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Tool> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.EnablingObjective_Tools).HasForeignKey(k => k.EOId).IsRequired();
            builder.HasOne(o => o.Tool).WithMany(m => m.EnablingObjective_Tools).HasForeignKey(k => k.ToolId).IsRequired();

            builder.HasIndex(i => new { i.EOId, i.ToolId }).IsUnique();
        }
    }
}
