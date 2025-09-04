using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
   public class TaskReview_StatusMap : Common.CommonMap<TaskReview_Status>
    {
        public override void Configure(EntityTypeBuilder<TaskReview_Status> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status);
        }
    }
}