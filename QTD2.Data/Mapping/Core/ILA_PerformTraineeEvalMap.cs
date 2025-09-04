using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_PerformTraineeEvalMap : Common.CommonMap<ILA_PerformTraineeEval>
    {
        public override void Configure(EntityTypeBuilder<ILA_PerformTraineeEval> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithOne(w => w.ILA_PerformTraineeEval).HasForeignKey<ILA_PerformTraineeEval>(f => f.ILAId).IsRequired();
        }
    }
}
