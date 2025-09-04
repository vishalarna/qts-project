using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_Set_LinkMap : Common.CommonMap<SafetyHazard_Set_Link>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_Set_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SafetyHazardSet).WithMany(m => m.SafetyHazard_Set_Links).HasForeignKey(k => k.SafetyHazardSetId).IsRequired();
            builder.HasOne(o => o.SafetyHazard).WithMany(m => m.SafetyHazard_Set_Links).HasForeignKey(k => k.SafetyHazardId).IsRequired();
            builder.HasIndex(i => new { i.SafetyHazardSetId, i.SafetyHazardId }).IsUnique();
        }
    }
}
