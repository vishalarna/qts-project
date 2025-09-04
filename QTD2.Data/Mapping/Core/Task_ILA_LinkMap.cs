using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_ILA_LinkMap : Common.CommonMap<Task_ILA_Link>
    {
        public override void Configure(EntityTypeBuilder<Task_ILA_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany(m => m.Task_ILA_Links).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.ILA).WithMany(m => m.Task_ILA_Links).HasForeignKey(k => k.ILAId).IsRequired();

            builder.HasIndex(i => new { i.ILAId, i.TaskId }).IsUnique();
        }
    }
}
