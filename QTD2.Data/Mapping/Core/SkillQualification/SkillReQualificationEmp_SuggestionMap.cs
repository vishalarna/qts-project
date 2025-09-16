using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillReQualificationEmp_SuggestionMap : Common.CommonMap<SkillReQualificationEmp_Suggestion>
    {
        public override void Configure(EntityTypeBuilder<SkillReQualificationEmp_Suggestion> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.EnablingObjective_Suggestion).WithMany(m => m.SkillReQualificationEmp_Suggestions).HasForeignKey(f => f.SkillSuggestionId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction);
            builder.HasOne(o => o.SkillQualification).WithMany(m => m.SkillReQualificationEmp_Suggestion).HasForeignKey(f => f.SkillQualificationId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.SkillReQualificationEmp_SuggestionAsEvaluator).HasForeignKey(f => f.EvaluatorId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
            builder.HasOne(o => o.Trainee).WithMany(m => m.SkillReQualificationEmp_SuggestionAsTrainee).HasForeignKey(f => f.TraineeId).OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction).IsRequired();
        }
    }
}
