using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluationQuestionMap : Common.CommonMap<StudentEvaluationQuestion>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluationQuestion> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.StudentEvaluationForm).WithMany(m => m.StudentEvaluationQuestions).HasForeignKey(k => k.EvalFormID).IsRequired();
            builder.Property(o => o.QuestionNumber).IsRequired();
            builder.Property(o => o.QuestionText).IsRequired().HasMaxLength(200);
        }
    }
}
