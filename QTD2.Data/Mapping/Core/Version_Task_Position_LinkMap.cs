using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_Task_Position_LinkMap : Common.CommonMap<Version_Task_Position_Link>
    {
        public override void Configure(EntityTypeBuilder<Version_Task_Position_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Version_Task).WithMany(k => k.Version_Task_Position_Links).HasForeignKey(x => x.Version_TaskId).IsRequired();
            builder.HasOne(o => o.Version_Position).WithMany(k => k.Version_Task_Position_Links).HasForeignKey(x => x.Version_PositionId).IsRequired();
        }
    }
}
