using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_AssessmentTool_LinkMap : Common.CommonMap<ILA_AssessmentTool_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_AssessmentTool_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_AssessmentTool_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.AssessmentTool).WithMany(m => m.ILA_AssessmentTool_Links).HasForeignKey(k => k.AssessmentToolId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.AssessmentToolId }).IsUnique();
        }
    }
}
