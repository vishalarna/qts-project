using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_TaskMap : Common.CommonMap<Version_EnablingObjective_Task>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Version_Number);
            builder.HasOne(o => o.Version_EnablingObjective).WithMany(m => m.Version_EnablingObjective_Tasks).HasForeignKey(k => k.Version_EnablingObjectiveId).IsRequired();
            builder.HasOne(o => o.Version_Task).WithMany(m => m.Version_EnablingObjective_Tasks).HasForeignKey(k => k.Version_TaskId).IsRequired();
        }
    }
}
