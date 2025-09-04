using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingIssue_ActionItemStatusMap : Common.CommonMap<TrainingIssue_ActionItemStatus>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue_ActionItemStatus> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status).IsRequired();
        }
    }
}
