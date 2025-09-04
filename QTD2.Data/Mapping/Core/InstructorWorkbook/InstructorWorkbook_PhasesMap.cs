using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    class InstructorWorkbook_PhasesMap : Common.CommonMap<InstructorWorkbook_Phases>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_Phases> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.CoursePhaseDescription).IsRequired();

        }

    }
}
