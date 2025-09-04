using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class ILA_StudentEvaluation_LinkMap : Common.CommonMap<ILA_StudentEvaluation_Link>
    {
        public override void Configure(EntityTypeBuilder<ILA_StudentEvaluation_Link> builder)
        {
            base.Configure(builder);
            builder.HasOne(o => o.ILA).WithMany(m => m.ILA_StudentEvaluation_Links).HasForeignKey(k => k.ILAId).IsRequired();
            //builder.HasOne(o => o.StudentEvaluationAvailability).WithMany(m => m.ILA_StudentEvaluation_Links).HasForeignKey(k => k.studentEvalAvailabilityID).IsRequired();
            //builder.HasOne(o => o.StudentEvaluationAudience).WithMany(m => m.ILA_StudentEvaluation_Links).HasForeignKey(k => k.studentEvalAudienceID).IsRequired();
            builder.HasOne(o => o.StudentEvaluationForm).WithMany(m => m.ILA_StudentEvaluation_Links).HasForeignKey(k => k.studentEvalFormID).IsRequired();
            //builder.HasIndex(i => new { i.ILAId, i.studentEvalFormID, i.studentEvalAvailabilityID, i.studentEvalAudienceID }).IsUnique();
        }
    }
}
