using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_RegulatoryRequirementMap : Common.CommonMap<Version_RegulatoryRequirement>
    {
        public override void Configure(EntityTypeBuilder<Version_RegulatoryRequirement> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Number).HasMaxLength(50).IsRequired();
            builder.Property(o => o.Title).IsRequired().HasMaxLength(200);
            builder.Property(o => o.Description).HasMaxLength(2000);
            builder.Property(o => o.RevisionNumber);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Uploads);
            builder.Property(o => o.HyperLink);
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(m => m.Version_RegulatoryRequirements).HasForeignKey(k => k.RegulatoryRequirementId).IsRequired();
        }
    }
}
