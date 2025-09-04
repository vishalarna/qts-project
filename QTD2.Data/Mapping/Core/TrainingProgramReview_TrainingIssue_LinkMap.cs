using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingProgramReview_TrainingIssue_LinkMap:Common.CommonMap<TrainingProgramReview_TrainingIssue_Link>
    {
        public override void Configure(EntityTypeBuilder<TrainingProgramReview_TrainingIssue_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.TrainingProgram).WithMany(m => m.TrainingProgramReview_TrainingIssue_Links).HasForeignKey(k => k.TrainingProgramId).IsRequired();
            builder.HasOne(o => o.TrainingIssue).WithMany().HasForeignKey(k => k.TrainingIssueId).IsRequired();
        }
    }
}
