using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class TrainingIssue_DriverSubTypeMap : Common.CommonMap<TrainingIssue_DriverSubType>
    {
        public override void Configure(EntityTypeBuilder<TrainingIssue_DriverSubType> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SubType).IsRequired();
            builder.Property(o => o.DriverTypeId).IsRequired();
            builder.HasOne(o => o.DriverType).WithMany(o=>o.DriverSubTypes).HasForeignKey(k => k.DriverTypeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
