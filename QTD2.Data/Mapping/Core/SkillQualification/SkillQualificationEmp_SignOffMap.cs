using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillQualificationEmp_SignOffMap : Common.CommonMap<SkillQualificationEmp_SignOff>
    {
        public override void Configure(EntityTypeBuilder<SkillQualificationEmp_SignOff> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EvaluationMethod).WithMany(m => m.SkillQualificationEmp_SignOff).HasForeignKey(f => f.EvaluationMethodId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.SkillQualification).WithMany(m => m.SkillQualificationEmp_SignOff).HasForeignKey(f => f.SkillQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.SkillQualificationEmp_SignOffAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.SkillQualificationEmp_SignOffAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();

        }
    }
}
