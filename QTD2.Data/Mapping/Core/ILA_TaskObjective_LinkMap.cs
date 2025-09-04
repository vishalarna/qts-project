using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_TaskObjective_LinkMap : Common.CommonMap<ILA_TaskObjective_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_TaskObjective_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_TaskObjective_Links).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.Task).WithMany(m => m.ILA_TaskObjective_Links).HasForeignKey(k => k.TaskId).IsRequired();
            builder.Property(p => p.UseForTQ).HasDefaultValue(false);
            builder.Property(p => p.SequenceNumber).HasDefaultValue(0);
            builder.HasIndex(i => new { i.ILAId, i.TaskId }).IsUnique().HasFilter("[Deleted] = 0");
        }
    }
}
