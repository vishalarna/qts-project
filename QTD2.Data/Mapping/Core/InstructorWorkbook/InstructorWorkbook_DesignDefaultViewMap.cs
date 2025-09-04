using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
   public class InstructorWorkbook_DesignDefaultViewMap : Common.CommonMap<InstructorWorkbook_DesignDefaultView>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_DesignDefaultView> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.DesignDefaultView);
        }
    }
}
