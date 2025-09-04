using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TaskListReview_TypeMap : Common.CommonMap<TaskListReview_Type>
    {
        public override void Configure(EntityTypeBuilder<TaskListReview_Type> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Type).IsRequired();
        }
    }
}
