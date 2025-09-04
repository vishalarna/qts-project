using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingIssue_ActionItemPriorityMap : Common.CommonMap<TrainingIssue_ActionItemPriority>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue_ActionItemPriority> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Priority).IsRequired();
        }
    }
}
