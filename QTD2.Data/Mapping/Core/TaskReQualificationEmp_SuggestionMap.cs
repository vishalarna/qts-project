using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TaskReQualificationEmp_SuggestionMap : Common.CommonMap<TaskReQualificationEmp_Suggestion>
    {
        public override void Configure(EntityTypeBuilder<TaskReQualificationEmp_Suggestion> builder)
        {
            base.Configure(builder);

         
            builder.HasOne(o => o.Task_Suggestion).WithMany(m => m.TaskReQualificationEmp_Suggestions).HasForeignKey(f => f.TaskSuggestionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.TaskQualification).WithMany(m => m.TaskReQualificationEmp_Suggestions).HasForeignKey(f => f.TaskQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.TaskReQualificationEmp_SuggestionsAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.TaskReQualificationEmp_SuggestionsAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
        }
    }
}
