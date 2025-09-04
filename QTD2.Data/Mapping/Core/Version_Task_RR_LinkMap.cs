using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_RR_LinkMap : Common.CommonMap<Version_Task_RR_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_RR_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_Task_RR_Links).HasForeignKey(k => k.Version_TaskId).IsRequired();
            builder.HasOne(o => o.Version_RegulatoryRequirement).WithMany(m => m.Version_Task_RR_Links).HasForeignKey(k => k.Version_RegulatoryRequirementId).IsRequired();
            builder.HasIndex(i => new { i.Version_RegulatoryRequirementId, i.Version_TaskId }).IsUnique();
        }
    }
}
