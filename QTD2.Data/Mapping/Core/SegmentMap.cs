using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SegmentMap : Common.CommonMap<Segment>
    {
        public override void Configure(EntityTypeBuilder<Segment> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired().HasMaxLength(500);
            builder.Property(o => o.Duration).IsRequired();
            builder.Property(o => o.Content).IsRequired();
            builder.Property(o => o.Uploads);
            builder.Property(o => o.IsNercStandard);
            builder.Property(o => o.IsNercOperatingTopics);
            builder.Property(o => o.IsNercSimulation);
            builder.Property(o => o.IsPartialCredit);
        }
    }
}
