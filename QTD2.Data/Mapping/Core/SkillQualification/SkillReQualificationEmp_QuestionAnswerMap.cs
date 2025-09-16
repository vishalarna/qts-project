using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillReQualificationEmp_QuestionAnswerMap : Common.CommonMap<SkillReQualificationEmp_QuestionAnswer>
    {
        public override void Configure(EntityTypeBuilder<SkillReQualificationEmp_QuestionAnswer> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective_Question).WithMany(m => m.SkillReQualificationEmp_QuestionAnswers).HasForeignKey(f => f.SkillQuestionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.SkillQualification).WithMany(m => m.SkillReQualificationEmp_QuestionAnswer).HasForeignKey(f => f.SkillQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.SkillReQualificationEmp_QuestionAnswerAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.SkillReQualificationEmp_QuestionAnswerAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
        }
    }
}
