using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class InstructorWorkbook_Segments_NercStandardsMap : Common.CommonMap<InstructorWorkbook_Segments_NercStandards>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_Segments_NercStandards> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.SegmentId).IsRequired();
            builder.Property(o => o.Standards).IsRequired();
            builder.Property(o => o.OperatingTopic).IsRequired();
            builder.Property(o => o.Simulation).IsRequired();
            builder.Property(o => o.ProfessionalCredit).IsRequired();
        }
    }
}
