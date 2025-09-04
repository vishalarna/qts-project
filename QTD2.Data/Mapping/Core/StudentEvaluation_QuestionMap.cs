using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluation_QuestionMap : Common.CommonMap<StudentEvaluation_Question>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluation_Question> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.StudentEvaluation).WithMany(x => x.StudentEvaluationQuestions).HasForeignKey(y => y.StudentEvaluationId).IsRequired();
            builder.HasOne(o => o.QuestionBank).WithMany(x => x.StudentEvaluationQuestions).HasForeignKey(y => y.QuestionBankId).IsRequired();
        }
    }
}
