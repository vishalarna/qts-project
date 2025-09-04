using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class TaskQualification_Evaluator_LinkMap : Common.CommonMap<TaskQualification_Evaluator_Link>
    {
        public override void Configure(EntityTypeBuilder<TaskQualification_Evaluator_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.TaskQualification).WithMany(m => m.TaskQualification_Evaluator_Links).HasForeignKey(f => f.TaskQualificationId).IsRequired();
            builder.HasOne(x => x.Evaluator).WithMany(m => m.TaskQualification_Evaluator_Links).HasForeignKey(f => f.EvaluatorId).IsRequired().OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
