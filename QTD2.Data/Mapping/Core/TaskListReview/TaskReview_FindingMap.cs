using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TaskReview_FindingMap : Common.CommonMap<TaskReview_Finding>
    {
        public override void Configure(EntityTypeBuilder<TaskReview_Finding> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Finding);
        }
    }
}
