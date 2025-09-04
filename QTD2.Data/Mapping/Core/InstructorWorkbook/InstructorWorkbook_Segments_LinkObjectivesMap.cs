using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QTD2.Domain.Entities.Core;

namespace QTD2.Data.Mapping.Core
{
    public class InstructorWorkbook_Segments_LinkObjectivesMap : Common.CommonMap<InstructorWorkbook_Segments_LinkObjectives>
    {
        public override void Configure(EntityTypeBuilder<InstructorWorkbook_Segments_LinkObjectives> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Type).IsRequired();
            builder.Property(o => o.Number).IsRequired();
            builder.Property(o => o.Description).IsRequired();
            builder.Property(o => o.Order).IsRequired();
        }
    }
}
