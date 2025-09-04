using QTD2.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Data.Mapping.Core
{
   public class DIFSurvey_EmployeeMap : Common.CommonMap<DIFSurvey_Employee>
    {
        public override void Configure(EntityTypeBuilder<DIFSurvey_Employee> builder)
        {
            base.Configure(builder);
            builder.Property(o => o.DIFSurveyId).IsRequired();
            builder.Property(o => o.EmployeeId).IsRequired();
            builder.Property(o => o.Started);
            builder.Property(o => o.Complete);
            builder.Property(o => o.StatusId).IsRequired();
            builder.Property(o => o.ReleaseDate);
            builder.Property(o => o.CompletedDate);
            builder.Property(o => o.Comments);

            builder.HasOne(o => o.DIFSurvey).WithMany(o=>o.Employees).HasForeignKey(f => f.DIFSurveyId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(o => o.Employee).WithMany(o=>o.DIFSurvey_Employees).HasForeignKey(f => f.EmployeeId).OnDelete(DeleteBehavior.Restrict); ;
            builder.HasOne(o => o.Status).WithMany(o=>o.DIFSurvey_Employees).HasForeignKey(f => f.StatusId).OnDelete(DeleteBehavior.Restrict); ;

        }
    }
}