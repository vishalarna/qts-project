using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QTD2.Data.Mapping.Core
{
    public class DIFSurvey_Employee_StatusMap : Common.CommonMap<DIFSurvey_Employee_Status>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_Employee_Status> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.Status).IsRequired();
        }
    }
}