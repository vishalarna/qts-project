using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILATraineeEvaluationMap : Common.CommonMap<ILATraineeEvaluation>
    {
        public override void Configure(EntityTypeBuilder<ILATraineeEvaluation> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.Test).WithMany(m => m.ILATraineeEvaluations).HasForeignKey(k => k.TestId).IsRequired();
            builder.HasOne(o => o.ILA).WithMany(m => m.ILATraineeEvaluations).HasForeignKey(k => k.ILAId).IsRequired();
            builder.HasOne(o => o.TestType).WithMany(m => m.ILATraineeEvaluations).HasForeignKey(k => k.TestTypeId);
            builder.HasOne(o => o.TraineeEvaluationType).WithMany(m => m.ILATraineeEvaluations).HasForeignKey(k => k.EvaluationTypeId).IsRequired();
        }
    }
}
