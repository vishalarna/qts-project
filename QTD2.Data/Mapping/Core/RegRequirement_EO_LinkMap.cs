using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class RegRequirement_EO_LinkMap : Common.CommonMap<RegRequirement_EO_Link>
    {
        public override void Configure(EntityTypeBuilder<RegRequirement_EO_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EO).WithMany(m => m.RegRequirement_EO_Links).HasForeignKey(k => k.EOID).IsRequired();
            builder.HasOne(o => o.RegulatoryRequirement).WithMany(m => m.RegRequirement_EO_Links).HasForeignKey(k => k.RegulatoryRequirementId).IsRequired();
            builder.HasIndex(i => new { i.EOID, i.RegulatoryRequirementId }).IsUnique();
        }
    }
}
