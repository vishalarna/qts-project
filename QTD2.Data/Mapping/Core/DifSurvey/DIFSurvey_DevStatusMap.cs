using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class DIFSurvey_DevStatusMap : Common.CommonMap<DIFSurvey_DevStatus>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_DevStatus> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status).IsRequired();
        }
    }
}