using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillQualificationMap : Common.CommonMap<SkillQualification>
    {
        public override void Configure(EntityTypeBuilder<SkillQualification> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EnablingObjectiveId).IsRequired();
            builder.Property(o => o.EmployeeId).IsRequired();
            builder.Property(o => o.EvaluationMethodId);
            builder.Property(o => o.SkillQualificationStatusId);
            builder.Property(o => o.ClassScheduleId);
            builder.Property(o => o.SkillQualificationDate);
            builder.Property(o => o.DueDate);
            builder.Property(o => o.RecallDate);
            builder.Property(o => o.CriteriaMet).IsRequired();
            builder.Property(o => o.Comments);
            builder.Property(o => o.IsReleasedToEMP).IsRequired();
            builder.Property(o => o.IsRecalled).IsRequired();
            builder.Property(o => o.Sequence);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.SkillQualifications).HasForeignKey(k => k.EnablingObjectiveId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.Employee).WithMany(m => m.SkillQualifications).HasForeignKey(k => k.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.SkillQualifications).HasForeignKey(k => k.ClassScheduleId).OnDelete(DeleteBehavior.NoAction);

        }
    }
}