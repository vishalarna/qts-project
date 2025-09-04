using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SegmentObjective_LinkMap : Common.CommonMap<SegmentObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<SegmentObjective_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Segment).WithMany(m => m.SegmentObjective_Links).HasForeignKey(k => k.SegmentId).IsRequired();
            builder.HasOne(o => o.Task).WithMany(m => m.SegmentObjective_Links).HasForeignKey(k => k.TaskId);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.SegmentObjective_Links).HasForeignKey(k => k.EnablingObjectiveId);
            builder.HasOne(o => o.CustomEnablingObjective).WithMany(m => m.SegmentObjective_Links).HasForeignKey(k => k.CustomEOId);
        }
    }
}
