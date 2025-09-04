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
    public class Task_MetaTask_LinkMap : Common.CommonMap<Task_MetaTask_Link>
    {
        public override void Configure(EntityTypeBuilder<Task_MetaTask_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task).WithMany().HasForeignKey(k => k.TaskId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.Meta_Task).WithMany(m => m.Task_MetaTask_Links).HasForeignKey(k => k.Meta_TaskId).IsRequired();

            builder.HasIndex(i => new { i.Meta_TaskId, i.TaskId }).IsUnique();
        }
    }
}
