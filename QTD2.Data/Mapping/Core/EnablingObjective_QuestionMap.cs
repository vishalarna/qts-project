using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_QuestionMap : Common.CommonMap<EnablingObjective_Question>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Question> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Question).IsRequired();
            builder.Property(o => o.Answer).IsRequired();
            builder.Property(o => o.QuestionNumber);
            builder.HasOne(o => o.EnablingObjective).WithMany(o => o.EnablingObjective_Questions).HasForeignKey(k => k.EnablingObjectiveId).IsRequired();
        }
    }
}
