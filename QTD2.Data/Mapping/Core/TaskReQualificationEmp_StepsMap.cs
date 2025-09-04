using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TaskReQualificationEmp_StepsMap : Common.CommonMap<TaskReQualificationEmp_Steps>
    {
        public override void Configure(EntityTypeBuilder<TaskReQualificationEmp_Steps> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task_Step).WithMany(m => m.TaskReQualificationEmp_Steps).HasForeignKey(f => f.TaskStepId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.TaskQualification).WithMany(m => m.TaskReQualificationEmp_Steps).HasForeignKey(f => f.TaskQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.TaskReQualificationEmp_StepsAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.TaskReQualificationEmp_StepsAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();

        }
    }
}
