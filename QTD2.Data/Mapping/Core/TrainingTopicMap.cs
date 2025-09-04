using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingTopicMap : Common.CommonMap<TrainingTopic>
    {
        public override void Configure(EntityTypeBuilder<TrainingTopic> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(500);
            builder.HasOne(o => o.TrainingTopic_Category).WithMany(m => m.TrainingTopics).HasForeignKey(k => k.TrainingTopic_CategoryId).IsRequired();
        }
    }
}
