using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class Version_TrainingGroupMap : Common.CommonMap<Version_TrainingGroup>
    {
        public override void Configure(EntityTypeBuilder<Version_TrainingGroup> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.CategoryId).IsRequired();
            builder.Property(o => o.GroupName).IsRequired();
            builder.Property(o => o.GroupNumber).IsRequired();
            builder.Property(o => o.GroupDescription);
            builder.Property(o => o.HyperLink);
            builder.Property(o => o.PDF);
            builder.HasOne(o => o.TrainingGroup).WithMany(m => m.Version_TrainingGroups).HasForeignKey(f => f.Version_TrainingGroupId).IsRequired();
        }
    }
}
