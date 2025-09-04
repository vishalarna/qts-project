using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluationMap : Common.CommonMap<StudentEvaluation>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluation> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Title).IsRequired().HasMaxLength(500);
            builder.Property(o => o.RatingScaleId).IsRequired();
            builder.Property(o => o.Instructions);
            builder.Property(o => o.IsPublished);
            builder.HasOne(o => o.RatingScaleN).WithMany(m => m.StudentEvaluations).HasForeignKey(k => k.RatingScaleId).IsRequired();

        }
    }
}
