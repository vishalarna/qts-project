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
    public class TaskQualificationMap : Common.CommonMap<TaskQualification>
    {
        public override void Configure(EntityTypeBuilder<TaskQualification> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.IsReleasedToEMP).HasDefaultValue(false);
            builder.Property(o => o.IsRecalled).HasDefaultValue(false);
            builder.Property(o => o.RecallDate);
            builder.HasOne(o => o.Task).WithMany(m => m.TaskQualifications).HasForeignKey(f => f.TaskId).IsRequired();
            builder.HasOne(o => o.Employee).WithMany(m => m.TaskQualifications).HasForeignKey(f => f.EmpId).IsRequired();
            builder.HasOne(o => o.EvaluationMethod).WithMany(m => m.TaskQualifications).HasForeignKey(f => f.EvaluationId);
            builder.HasOne(o => o.TaskQualificationStatus).WithMany(m => m.TaskQualifications).HasForeignKey(f => f.TQStatusId);
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.TaskQualifications).HasForeignKey(f => f.ClassScheduleId);
            //builder.HasOne(o => o.Evaluator).WithMany().HasForeignKey(f => f.EvaluatorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
