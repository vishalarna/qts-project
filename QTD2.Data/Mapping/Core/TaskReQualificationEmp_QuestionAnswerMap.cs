using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TaskReQualificationEmp_QuestionAnswerMap : Common.CommonMap<TaskReQualificationEmp_QuestionAnswer>
    {
        public override void Configure(EntityTypeBuilder<TaskReQualificationEmp_QuestionAnswer> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Task_Question).WithMany(m => m.TaskReQualificationEmp_QuestionAnswers).HasForeignKey(f => f.TaskQuestionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.TaskQualification).WithMany(m => m.TaskReQualificationEmp_QuestionAnswers).HasForeignKey(f => f.TaskQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.TaskReQualificationEmp_QuestionAnswersAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.TaskReQualificationEmp_QuestionAnswersAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();

        }
    }
}
