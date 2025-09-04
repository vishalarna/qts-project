using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class SkillQualification_Evaluator_LinkMap : Common.CommonMap<SkillQualification_Evaluator_Link>
    {
        public override void Configure(EntityTypeBuilder<SkillQualification_Evaluator_Link> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.EvaluatorId).IsRequired();
            builder.Property(o => o.SkillQualificationId).IsRequired();
            builder.Property(o => o.Number).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.SkillQualification_Evaluator_Links).HasForeignKey(k => k.EvaluatorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(o => o.SkillQualification).WithMany(m => m.SkillQualification_Evaluator_Links).HasForeignKey(k => k.SkillQualificationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
