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
    public class ClassSchedule_Evaluator_LinkMap : Common.CommonMap<ClassSchedule_Evaluator_Link>
    {
        public override void Configure(EntityTypeBuilder<ClassSchedule_Evaluator_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ClassSchedule).WithMany(m => m.ClassSchedule_Evaluator_Links).HasForeignKey(k => k.ClassScheduleId).IsRequired();
            builder.HasOne(o => o.Evaluator).WithMany(m => m.ClassSchedule_Evaluator_Links).HasForeignKey(k => k.EvaluatorId).IsRequired();
        }
    }
}
