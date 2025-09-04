using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Task_Reference_LinkMap : Common.CommonMap<Task_Reference_Link>
    {
        public override void Configure(EntityTypeBuilder<Task_Reference_Link> builder)
        {
            base.Configure(builder);

            builder.HasOne(o => o.Task).WithMany(m => m.Task_Reference_Links).HasForeignKey(k => k.TaskId).IsRequired();
            builder.HasOne(o => o.Task_Reference).WithMany(m => m.Task_Reference_Links).HasForeignKey(k => k.TaskReferenceId).IsRequired();
            builder.HasIndex(i => new { i.TaskReferenceId, i.TaskId }).IsUnique();
        }
    }
}
