using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TaskQualificationStatusMap : Common.CommonMap<TaskQualificationStatus>
    {
        public override void Configure(EntityTypeBuilder<TaskQualificationStatus> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name);
            builder.Property(o => o.Description);
        }
    }
}
