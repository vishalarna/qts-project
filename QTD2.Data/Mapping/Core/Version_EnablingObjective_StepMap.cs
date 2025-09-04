using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class Version_EnablingObjective_StepMap : Common.CommonMap<Version_EnablingObjective_Step>
    {
        public override void Configure(EntityTypeBuilder<Version_EnablingObjective_Step> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number).IsRequired();

            builder.HasOne(o => o.EnablingObjective_Step).WithMany(m => m.Version_EnablingObjective_Steps).HasForeignKey(k => k.EOStepId).IsRequired();
        }
    }
}
