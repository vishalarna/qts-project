using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SaftyHazard_RR_LinkMap : Common.CommonMap<SaftyHazard_RR_Link>
    {
        public override void Configure(EntityTypeBuilder<SaftyHazard_RR_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.SaftyHazard_RR_Links).HasForeignKey(k => k.SafetyHazardId).IsRequired();
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(m => m.SaftyHazard_RR_Links).HasForeignKey(k => k.RegulatoryRequirementId).IsRequired();

            builder.HasIndex(i => new { i.SafetyHazardId, i.RegulatoryRequirementId }).IsUnique();
        }
    }
}
