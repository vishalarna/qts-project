using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_Tool_LinkMap : Common.CommonMap<SafetyHazard_Tool_Link>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_Tool_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.SafetyHazard_Tool_Links).HasForeignKey(k => k.SafetyHazardId).IsRequired();
            builder.HasOne(o => o.Tool).WithMany(m => m.SafetyHazard_Tool_Links).HasForeignKey(k => k.ToolId).IsRequired();
        }
    }
}
