using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Mapping.Core
{
    public class ProcedureReviewMap : Common.CommonMap<ProcedureReview>
    {
        public override void Configure(EntityTypeBuilder<ProcedureReview> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ExtensionType);
            builder.Property(x => x.ExtensionAmount);
            builder.HasOne(x => x.Procedure).WithMany(m => m.ProcedureReviews).HasForeignKey(y => y.ProcedureId);

        }
    }
}
