using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_Segment_LinkMap : Common.CommonMap<ILA_Segment_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_Segment_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_Segment_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.Segment).WithMany(m => m.ILA_Segment_Links).HasForeignKey(k => k.SegmentId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.SegmentId }).IsUnique();
        }
    }
}
