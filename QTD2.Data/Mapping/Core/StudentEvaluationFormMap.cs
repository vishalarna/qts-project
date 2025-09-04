using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class StudentEvaluationFormMap : Common.CommonMap<StudentEvaluationForm>
    {
        public override void Configure(EntityTypeBuilder<StudentEvaluationForm> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Name).IsRequired().HasMaxLength(50);
            builder.Property(o => o.IsShared).HasDefaultValue(false);
            builder.Property(o => o.IsNAOption).HasDefaultValue(false);
            builder.Property(o => o.IncludeComments).HasDefaultValue(false);
            builder.Property(o => o.IsAvailableForAllILAs).HasDefaultValue(false);

            builder.HasOne(o => o.RatingScale).WithMany(m => m.StudentEvaluationForms).HasForeignKey(k => k.RatingScaleId).IsRequired();
        }
    }
}
