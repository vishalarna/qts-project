using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingTopic_CategoryMap : Common.CommonMap<TrainingTopic_Category>
    {
        public override void Configure(EntityTypeBuilder<TrainingTopic_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(200);
        }
    }
}
