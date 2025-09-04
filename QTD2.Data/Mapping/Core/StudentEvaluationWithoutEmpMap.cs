using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluationWithoutEmpMap : Common.CommonMap<StudentEvaluationWithoutEmp>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluationWithoutEmp> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.StudentEvaluation).WithMany(m => m.StudentEvaluationWithoutEmps).HasForeignKey(k => k.StudentEvaluationId).IsRequired();
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.StudentEvaluationWithoutEmps).HasForeignKey(k => k.ClassScheduleId).IsRequired();
            builder.HasOne(o => o.QuestionBank).WithMany(m => m.StudentEvaluationWithoutEmps).HasForeignKey(k => k.QuestionId).IsRequired();
            builder.HasOne(o => o.Employee).WithMany(m => m.StudentEvaluationWithoutEmps).HasForeignKey(k => k.EmployeeId);
            builder.HasOne(o => o.RatingScaleExpanded).WithMany(m => m.StudentEvaluationWithoutEmps).HasForeignKey(k => k.RatingScale);
        }
    }
}
