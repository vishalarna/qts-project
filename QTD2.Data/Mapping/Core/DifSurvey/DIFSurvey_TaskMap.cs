using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
   public class DIFSurvey_TaskMap : Common.CommonMap<DIFSurvey_Task>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_Task> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DifSurveyId).IsRequired();
            builder.Property(o => o.TaskId).IsRequired();
            builder.Property(o => o.AverageDifficulty);
            builder.Property(o => o.AverageImportance);
            builder.Property(o => o.AverageFrequency);
            builder.Property(o => o.TrainingStatus_CalculatedId).IsRequired();
            builder.Property(o => o.TrainingStatus_OverrideId);
            builder.Property(o => o.Comments);
            builder.Property(o => o.CommentingEmployeeId);
            builder.Property(o => o.DIFSurvey_Task_TrainingFrequencyId);

            builder.HasOne(o => o.DifSurvey).WithMany(o=>o.Tasks).HasForeignKey(f => f.DifSurveyId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Task).WithMany(o =>o.DIFSurvey_Tasks).HasForeignKey(f => f.TaskId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.TrainingStatus_Calculated).WithMany().HasForeignKey(f => f.TrainingStatus_CalculatedId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.TrainingStatus_Override).WithMany().HasForeignKey(f => f.TrainingStatus_OverrideId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.CommentingEmployee).WithMany(o=>o.DIFSurvey_Tasks).HasForeignKey(f => f.CommentingEmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.DIFSurvey_Task_TrainingFrequency).WithMany().HasForeignKey(f => f.DIFSurvey_Task_TrainingFrequencyId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}