using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class SafetyHazard_ILA_LinkMap : Common.CommonMap<SafetyHazard_ILA_Link>
    {
        public override void Configure(EntityTypeBuilder<SafetyHazard_ILA_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.SafetyHazard_ILA_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.SaftyHazard).WithMany(m => m.SafetyHazard_ILA_Links).HasForeignKey(k => k.SafetyHazardId).IsRequired();
            builder.HasIndex(i => new { i.ILAId, i.SafetyHazardId }).IsUnique();
        }
    }
}
