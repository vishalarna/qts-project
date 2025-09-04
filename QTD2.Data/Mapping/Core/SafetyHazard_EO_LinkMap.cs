using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_EO_LinkMap : Common.CommonMap<SafetyHazard_EO_Link>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_EO_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.SafetyHazard_EO_Links).HasForeignKey(k => k.SafetyHazardId).IsRequired();
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.SafetyHazard_EO_Links).HasForeignKey(k => k.EOID).IsRequired();

            builder.HasIndex(i => new { i.SafetyHazardId, i.EOID }).IsUnique();
        }
    }
}
