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
    public class Version_Task_MetaTask_LinkMap : Common.CommonMap<Version_Task_MetaTask_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_MetaTask_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Version_Task).WithMany().HasForeignKey(k => k.Version_TaskId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(o => o.Version_MetaTask).WithMany(m => m.Version_Task_MetaTask_Links).HasForeignKey(k => k.Version_MetaTaskId).IsRequired();
        }
    }
}
