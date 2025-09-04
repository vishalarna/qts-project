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
    public class Task_HistoryMap : Common.CommonMap<Task_History>
    {
        public override void Configure(EntityTypeBuilder<Task_History> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.OldStatus).HasDefaultValue(false);
            builder.Property(o => o.NewStatus).HasDefaultValue(true);
            builder.Property(o => o.ChangeEffectiveDate);
            builder.Property(o => o.ChangeNotes);
            builder.HasOne(o => o.Task).WithMany(x => x.Task_Histories).HasForeignKey(y => y.TaskId).IsRequired();
            builder.HasOne(o => o.Version_Task).WithMany(x => x.Task_Histories).HasForeignKey(y => y.Version_TaskId).OnDelete(DeleteBehavior.Restrict).IsRequired();
        }
    }
}
