using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_ILAEvaluation_DefaultViewMap : Common.CommonMap<InstructorWorkbook_ILAEvaluation_DefaultView>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_ILAEvaluation_DefaultView> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DefaultView).IsRequired();

        }

    }
}
