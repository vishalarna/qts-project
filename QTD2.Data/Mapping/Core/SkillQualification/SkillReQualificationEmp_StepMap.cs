using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillReQualificationEmp_StepMap : Common.CommonMap<SkillReQualificationEmp_Step>
    {
        public override void Configure(EntityTypeBuilder<SkillReQualificationEmp_Step> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective_Step).WithMany(m => m.SkillReQualificationEmp_Steps).HasForeignKey(f => f.SkillStepId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.SkillQualification).WithMany(m => m.SkillReQualificationEmp_Step).HasForeignKey(f => f.SkillQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.SkillReQualificationEmp_StepAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.SkillReQualificationEmp_StepAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
        }
    }
}
