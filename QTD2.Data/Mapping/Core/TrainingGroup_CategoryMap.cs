using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingGroup_CategoryMap : Common.CommonMap<TrainingGroup_Category>
    {
        public override void Configure(EntityTypeBuilder<TrainingGroup_Category> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired();
            builder.Property(o => o.Description);
            builder.Property(o => o.EffectiveDate);
            builder.Property(o => o.Note);
        }
    }
}
