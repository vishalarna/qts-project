using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_QuestionMap : Common.CommonMap<Version_EnablingObjective_Question>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_Question> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Question).IsRequired();
            builder.Property(o => o.Answer).IsRequired();

            //builder.HasOne(o => o.Version_EnablingObjective).WithMany(m => m.Version_EnablingObjective_Questions).HasForeignKey(k => k.Version_EnablingObjectiveId).IsRequired();
            builder.HasOne(o => o.EnablingObjective_Question).WithMany(m => m.Version_EnablingObjective_Questions).HasForeignKey(k => k.EOQuestionId).IsRequired();
        }
    }
}
