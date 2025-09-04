using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class EnablingObjective_StepMap : Common.CommonMap<EnablingObjective_Step>
    {
        public override void Configure(EntityTypeBuilder<EnablingObjective_Step> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Number);
            builder.Property(o => o.ParentStepId);
            builder.HasOne(o => o.EnablingObjective).WithMany(m => m.EnablingObjective_Steps).HasForeignKey(k => k.EOId).IsRequired();

        }
    }
}
