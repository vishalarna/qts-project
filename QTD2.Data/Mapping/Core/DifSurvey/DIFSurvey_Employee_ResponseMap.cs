using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
    public class DIFSurvey_Employee_ResponseMap : Common.CommonMap<DIFSurvey_Employee_Response>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_Employee_Response> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DIFSurvey_EmployeeId).IsRequired();
            builder.Property(o => o.DIFSurvey_TaskId).IsRequired();
            builder.Property(o => o.Difficulty);
            builder.Property(o => o.Importance);
            builder.Property(o => o.Frequency);
            builder.Property(o => o.NA).IsRequired();
            builder.Property(o => o.Comments);

            builder.HasOne(o => o.DIFSurvey_Employee).WithMany(o =>o.Responses).HasForeignKey(f => f.DIFSurvey_EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.DIFSurvey_Task).WithMany(o=>o.Responses).HasForeignKey(f => f.DIFSurvey_TaskId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}